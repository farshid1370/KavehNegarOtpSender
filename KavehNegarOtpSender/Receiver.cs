using ExcelMapper;

namespace KavehNegarOtpSender;

public class Receiver
{
    public string PhoneNumber { get; set; }
    public string? Token { get; set; }
    public string? Token1 { get; set; }
    public string? Token2 { get; set; }
}

public class ReceiverClassMap : ExcelClassMap<Receiver>
{
    public ReceiverClassMap()
    {
        Map(receiver => receiver.PhoneNumber).WithColumnIndex(0);


    }
}
public class ReceiverClassMap1 : ExcelClassMap<Receiver>
{
    public ReceiverClassMap1()
    {
        Map(receiver => receiver.PhoneNumber).WithColumnIndex(0);
        Map(receiver => receiver.Token).WithColumnIndex(1).WithInvalidFallback(null);


    }
}
public class ReceiverClassMap2 : ExcelClassMap<Receiver>
{
    public ReceiverClassMap2()
    {
        Map(receiver => receiver.PhoneNumber).WithColumnIndex(0);
        Map(receiver => receiver.Token).WithColumnIndex(1).WithInvalidFallback(null);
        Map(receiver => receiver.Token1).WithColumnIndex(2).WithInvalidFallback(null);


    }
}
public class ReceiverClassMap3 : ExcelClassMap<Receiver>
{
    public ReceiverClassMap3()
    {
        Map(receiver => receiver.PhoneNumber).WithColumnIndex(0);
        Map(receiver => receiver.Token).WithColumnIndex(1).WithInvalidFallback(null);
        Map(receiver => receiver.Token1).WithColumnIndex(2).WithInvalidFallback(null);
        Map(receiver => receiver.Token2).WithColumnIndex(3).WithInvalidFallback(null);

    }
}