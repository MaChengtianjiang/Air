// 这里应该继承一个什么通用类

[System.Serializable]
public class PlayerParam {
    // 体能
    public int Str;

    // 智力
    public int Int;

    // 道德
    public int Moral;

    // 魅力
    public int Charm;

    // 回合数
    public int Rounds;

    public int Age {
        get => Rounds / 4 + 7;
    }

//傲慢 
    public int Pride;

//贪婪
    public int Greed;

//色欲
    public int Lust;

//嫉妒
    public int Envy;

//暴食
    public int Gluttony;

//愤怒
    public int Wrath;

//懒惰
    public int Sloth;

    public PlayerParam() {
        Rounds = 1;

        Str = 5;
        Int = 5;
        Moral = 5;
        Charm = 5;
        
        Wrath = 50;
        Pride = 50;
        Greed = 50;
        Lust = 50;
        Envy = 50;
        Gluttony = 50;
        Sloth = 50;
    }
}