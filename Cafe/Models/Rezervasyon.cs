namespace Cafe.Models
{
	public class Rezervasyon
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string TelefonNo { get; set; }
        public int Sayı { get; set; }
        public string Saat { get; set; }
        public DateTime Tarih { get; set; }
    }
}
