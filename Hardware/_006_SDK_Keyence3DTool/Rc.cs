namespace NovaVision.Hardware._006_SDK_Keyence3DTool
{
    public enum Rc
    {
        Ok = 0,
        ErrOpenDevice = 4096,
        ErrNoDevice = 4097,
        ErrSend = 4098,
        ErrReceive = 4099,
        ErrTimeout = 4100,
        ErrNomemory = 4101,
        ErrParameter = 4102,
        ErrRecvFmt = 4103,
        ErrHispeedNoDevice = 4105,
        ErrHispeedOpenYet = 4106,
        ErrHispeedRecvYet = 4107,
        ErrBufferShort = 4108
    }
}
