using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace SonOfRadArrNotifications.Services;

public class SESService
{
    private readonly IAmazonSimpleEmailService _ses;

    private readonly string _fromAddress;

    public SESService()
    {
        var credentials = new BasicAWSCredentials("accessKey", "secretKey");
        _ses = new AmazonSimpleEmailServiceClient(credentials);
        
        _fromAddress = "email@email.com";
    }
        
    
    // Adopted from here: https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/csharp_ses_code_examples.html
    public async Task SendEmail(string address, string subject, string body)
    {
        try
        {
            var resp = await _ses.SendEmailAsync(new SendEmailRequest()
            {
                Destination = new Destination()
                {
                    ToAddresses = [address]
                },
                Message = new Message()
                {
                    Body = new Body()
                    {
                        Html = new Content()
                        {
                            Charset = "UTF-8",
                            Data = body
                        }
                    },
                    Subject = new Content()
                    {
                        Charset = "UTF-8",
                        Data = subject
                    }
                },
                Source = _fromAddress
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Sending email failed with exception: " + ex.Message);
        }
    }
    
}