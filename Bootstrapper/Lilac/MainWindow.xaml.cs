using System;
using log4net;
using System.IO;
using NHibernate;
using System.Windows;
using Nefarian.Startup;
using System.Reflection;
using System.Threading;
using Nefarian.Exchange;
using System.Configuration;
using FluentNHibernate.Cfg;
using System.Windows.Forms;
using Microsoft.Practices.Unity;


namespace Lilac
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "服务已启动";
            notifyIcon.Text = this.Title;
            Assembly asm = this.GetType().Assembly;
            Stream iocStream = asm.GetManifestResourceStream("Lilac.favicon.ico");
            notifyIcon.Icon = new System.Drawing.Icon(iocStream);
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000);
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            txtMsg.AppendText("启动中...");
            txtMsg.AppendText(Environment.NewLine);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ThreadPool.QueueUserWorkItem(o =>
            {
                Bootstrapper boot = new Bootstrapper();
                boot.PreInitModules += Boot_PreInitModules;
                boot.OnServiceOpened += Boot_OnServiceOpened;
                try
                {
                    boot.Run();
                    Dispatcher.Invoke(() => {
                        txtMsg.AppendText(Environment.NewLine);
                        txtMsg.AppendText("启动完毕。");
                        this.Visibility = Visibility.Hidden;
                    });
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    {
                        System.Windows.MessageBox.Show("启动出现错误：" + Environment.NewLine + ex.ToString(), string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            });


        }
        private void Boot_OnServiceOpened(object sender, ServiceOpenEventArgs e)
        {
            Dispatcher.Invoke(() => {
                foreach (Uri u in e.EndpointsAddress)
                {
                    txtMsg.AppendText(u.AbsoluteUri);
                    txtMsg.AppendText(Environment.NewLine);
                }
            });
        }

        private void Boot_PreInitModules(object sender, BootstrapperEventArgs e)
        {
            string connectionStr = ConfigurationManager.ConnectionStrings["MsSql"].ConnectionString;
            FluentConfiguration config = Fluently.Configure();
            foreach (Nefarian.Configuration.ModuleConfiguration module in e.Configuration.Modules)
            {
                config.Mappings(x => x.FluentMappings.AddFromAssembly(module.ModuleType.Assembly));
            }
            //config.Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(connectionStr));
            config.Database(FluentNHibernate.Cfg.Db.MySQLConfiguration.Standard.ConnectionString(connectionStr));

            config.CurrentSessionContext("wcf_operation");
            ISessionFactory sessionFactory;
            bool debugSQL = true;
            bool.TryParse(ConfigurationManager.AppSettings["DebugSQL"], out debugSQL);
            if (debugSQL)
            {
                NHibernate.Cfg.Configuration nhibernateConfig = config.BuildConfiguration();
                nhibernateConfig.Properties["show_sql"] = "true";
                nhibernateConfig.Properties["format_sql"] = "true";
                sessionFactory = nhibernateConfig.BuildSessionFactory();
            }
            else
            {
                sessionFactory = config.BuildSessionFactory();
            }
            IUnityContainer appContainer = Nefarian.Core.WebServiceSite.GetAppContainer();
            appContainer.RegisterInstance<ISessionFactory>("Lilac", sessionFactory);
            ExchangeCenter exchange = new ExchangeCenter();
            appContainer.RegisterInstance<ExchangeCenter>(exchange);

            MessagePublisher publisher = new MessagePublisher();
            appContainer.RegisterInstance<MessagePublisher>(publisher);

            log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger("Lilac");
            appContainer.RegisterInstance<ILog>(log);
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                Exception ex = (Exception)e.ExceptionObject;
                IUnityContainer container = Nefarian.Core.WebServiceSite.GetAppContainer();
                ILog log = container.Resolve<ILog>();
                string msg = "<未捕获的异常>";
                log.Error(msg, ex);
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
#if !DEBUG
            if (running) {
                MessageBoxResult res = System.Windows.MessageBox.Show("服务正在运行，确认关闭吗？", "注意", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (res == MessageBoxResult.Cancel) {
                    e.Cancel = true;
                }
            }
#endif
        } 

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                //最小化
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                    this.WindowState = WindowState.Normal;
                }
            }
        }
    }
}
