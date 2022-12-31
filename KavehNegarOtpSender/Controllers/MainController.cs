using System.Text;
using Microsoft.AspNetCore.Mvc;
using ExcelMapper;

namespace KavehNegarOtpSender.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MainController : ControllerBase
{
    private readonly string _nonBrakingSpace = ((char)0x00a0).ToString();
    [HttpPost]
    public async Task<IActionResult> SendOtp(IFormFile file, string apiKey, string template, int tokenCount = 0)
    {
        List<Receiver> receivers;
        try
        {
            if (file.Length <= 0)
            {
                return BadRequest();
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            await using var stream = file.OpenReadStream();
            using var importer = new ExcelImporter(stream);
            switch (tokenCount)
            {

                case 1:
                    importer.Configuration.RegisterClassMap<ReceiverClassMap1>();
                    break;
                case 2:
                    importer.Configuration.RegisterClassMap<ReceiverClassMap2>();
                    break;
                case 3:
                    importer.Configuration.RegisterClassMap<ReceiverClassMap3>();
                    break;
                default:
                    importer.Configuration.RegisterClassMap<ReceiverClassMap>();
                    break;
            }

            var sheet = importer.ReadSheet();
            sheet.HasHeading = false;
            receivers = sheet.ReadRows<Receiver>().ToList();
        }
        catch (Exception ex)
        {
            return Problem("Open Excel Failed");
        }

        try
        {


            var api = new Kavenegar.KavenegarApi(apiKey);

            foreach (var receiver in receivers)
            {
                receiver.Token = receiver.Token?.Replace(" ", _nonBrakingSpace);
                receiver.Token1 = receiver.Token1?.Replace(" ", _nonBrakingSpace);
                receiver.Token2 = receiver.Token2?.Replace(" ", _nonBrakingSpace);
                var result = await api.VerifyLookup(receiver.PhoneNumber, receiver.Token, receiver.Token1, receiver.Token2, template);

            }

        }
        catch (Exception ex)
        {
            return Problem("Send Otp Failed");
        }


        return Ok();
    }


}


