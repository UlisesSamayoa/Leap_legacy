namespace LEAP.Data
{
    // Viaja serializado dentro de FormsAuthenticationTicket.UserData (unico slot
    // barato por-request, ya que la app deliberadamente no usa sesion de
    // servidor). Antes ese campo guardaba solo el token crudo; ahora tambien
    // lleva el rol/CDS para poder restringir accesos sin otro round-trip a
    // leap_api en cada request.
    public class AuthTicketData
    {
        public string Token { get; set; }
        public string TypeUser { get; set; }
        public int? SpecialistId { get; set; }
        public string SpecialistName { get; set; }
    }
}
