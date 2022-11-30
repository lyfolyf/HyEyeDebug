using System;
using System.Windows.Forms;

namespace HyEye.WForm
{
    public partial class FrmLoading : Form
    {
        public event Action<string, int> LoadingProgressChanged;
        public FrmLoading()
        {
            InitializeComponent();

            //Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            label1.Text = "系统加载中，请稍后...";

            lblName.Text = $"{Global.Name}";

            lblVersion.Text = $"Version [{Global.Version}]";

            LoadingProgressChanged += FrmLoading_OnLoadingProgressChanged;
        }

        private void FrmLoading_OnLoadingProgressChanged(string message, int progress)
        {
            this.Invoke(new Action(() =>
            {
                lblInfo.Text = message;
                urProce.Value = progress;
                urProce.Text = $"{progress}%";
                this.Update();
            }));

            if (progress >= 100)
            {
                DialogResult = DialogResult.OK;
                this.Invoke(new Action(() => { Close(); }));
            }
        }

        public FrmLoading(string licType, int leftTime)
        {
            InitializeComponent();

            Global.LicType = licType;

            //Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            label1.Text = "系统加载中，请稍后...";

            lblName.Text = $"{Global.Name}";

            lblVersion.Text = $"Version [{Global.Version}] {Global.LicType} 剩余天数[{leftTime}]";

            LoadingProgressChanged += FrmLoading_OnLoadingProgressChanged;
        }

        public void Refresh(string message, int progress)
        {
            LoadingProgressChanged?.Invoke(message, progress);
        }
    }
}
