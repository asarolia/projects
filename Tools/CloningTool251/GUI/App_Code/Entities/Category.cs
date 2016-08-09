public class Category
{
    private string key, val;
    public string Key { 
        get{
            return key;
        }
        set {
            key = value;
        }
    }
    public string Value {
        get{
            return val;
        }
        set {
            val = value;
        }
    }

	public Category()
	{
	}

    public Category(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
