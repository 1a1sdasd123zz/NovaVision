using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Helper;

namespace NovaVision.BaseClass.Module
{
    public interface IElement : IChangedEvent
    {
        string Name { get; set; }

        string Type { get; set; }

        XmlObject Value { get; set; }
    }
}
