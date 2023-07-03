namespace OMD_TechX.Modelos
{
    public class Dia
    {
        public int Id { get; set; }
        public DateTime FechaEvent { get; set; } = new DateTime(1900, 1, 1);
        public DateTime FechaDesde { get; set; } = new DateTime(1900, 1, 1);
        public DateTime FechaHasta { get; set; } = new DateTime(1900, 1, 1);
        public string FechaValue { get; set; }
        public string DiaName { get; set;}

        public string VetDeTurno { get; set; }
    }
}
