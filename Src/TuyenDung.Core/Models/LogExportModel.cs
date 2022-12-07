namespace TuyenDung.Core.Models
{
    public class BaseStt
    {
        public int STT { get; set; }
    }

    public class LogExportModel : BaseStt
    {
        public string Name { get; set; }

        public string Active { get; set; }
    }
}