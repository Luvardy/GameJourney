public class DialogData
{
    public int type;
    public string pos;
    public string charName;
    public string content;
    public string picName;

    
    public DialogData(int type, string pos, string char_name, string content, string picName)
    {
        this.type = type;
        this.pos = pos;
        this.charName = char_name;
        this.content = content;
        this.picName = picName;
    }

    public DialogData(int type, string picName)
    {
        this.type = type;
        this.picName = picName;
    }

    public DialogData(int type, string char_name, string content)
    {
        this.type = type;
        this.charName = char_name;
        this.content = content;
    }
}