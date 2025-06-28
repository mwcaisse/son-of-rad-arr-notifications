using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Serilog;
using SonOfRadArrNotifications.Configuration;

namespace SonOfRadArrNotifications.Services;

public class SESService
{
    private readonly EmailConfiguration _emailConfiguration;
    
    private readonly IAmazonSimpleEmailService _ses;


    public SESService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;

        var credentials = new BasicAWSCredentials(_emailConfiguration.AccessKey, _emailConfiguration.SecretKey);
        _ses = new AmazonSimpleEmailServiceClient(credentials, Amazon.RegionEndpoint.GetBySystemName(_emailConfiguration.Region));
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
                Source = _emailConfiguration.FromAddress
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error sending email");
        }
    }
    
}