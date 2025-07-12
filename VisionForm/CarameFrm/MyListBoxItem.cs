namespace NovaVision.VisionForm.CarameFrm;

internal class MyListBoxItem
{
    private string Camdesc;

    private bool itemData;

    public bool ItemData
    {
        get
        {
            return itemData;
        }
        set
        {
            itemData = value;
        }
    }

    public MyListBoxItem(string Text)
    {
        Camdesc = Text;
        itemData = false;
    }

    public MyListBoxItem(string Text, bool ItemData)
    {
        Camdesc = Text;
        itemData = ItemData;
    }

    public override string ToString()
    {
        return Camdesc;
    }
}