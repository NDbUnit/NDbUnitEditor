using System;
using System.Windows.Forms;

namespace NDbUnitDataEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ComponentRegistrar registrar = new ComponentRegistrar();
            registrar.RegisterComponents();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataEditorPresenter presenter = IoC.Resolve<DataEditorPresenter>();
            presenter.Start();
        }

    }
}