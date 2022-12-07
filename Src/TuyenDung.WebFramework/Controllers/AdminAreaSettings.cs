using System.Collections;

namespace TuyenDung.WebFramework.Controllers
{
    public class AdminAreaSettings : ISettings
    {
        public AdminAreaSettings()
        {
            GridPageSize = 50;
            GridButtonCount = 5;
            GridPageSizeOptions = new[] { "50", "100", "200", "500" };
            RichEditorFlavor = "RichEditor";
        }

        public int GridPageSize { get; set; }

        public int GridButtonCount { get; set; }

        public IEnumerable GridPageSizeOptions { get; set; }

        public string RichEditorFlavor { get; set; }
    }
}