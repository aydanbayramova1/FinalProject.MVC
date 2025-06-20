namespace FinalProjectMvc.Services.Interfaces
{
    public interface IEmailService
    {
        //void Send(string to, string subject, string html, string from = null);


        Task SendAsync(string to, string subject, string html, string from = null);
    }
}
