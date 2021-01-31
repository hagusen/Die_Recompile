using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum faceType {
    Happy,
    Sad,
    Mad,
    Love,
    Party,
    Weird
}

public static class Faces {

    static string[] happyFaces =
    { "(✿◠‿◠)", "≧◡≦", "(▰˘◡˘▰)"
    , "(●´ω｀●)", "(ﾉ◕ヮ◕)ﾉ*:･ﾟ✧", "（ミ￣ー￣ミ）"
    , "(づ｡◕‿‿◕｡)づ", "✌.ʕʘ‿ʘʔ.✌", "◎[▪‿▪]◎"};

    static string[] sadFaces =
    { "ಥ_ಥ", "┐(‘～`；)┌", "◄.►"
    , "(◕︵◕)", "v( ‘.’ )v", "ਉ_ਉ"
    , "o(╥﹏╥)o", "●︿●", "(∩︵∩)"};

    static string[] madFaces =
    { "〴⋋_⋌〵", "(◣_◢)", "ლ(́◉◞౪◟◉‵ლ"
    , "ಠ益ಠ", "☉▵☉凸", "ↁ_ↁ"
    , "╚(•⌂•)╝", "ᇂﮌᇂ)", "(┛◉Д◉)┛彡┻━┻ "};
    static string[] loveFaces =
    { "v(=∩_∩=)ﾌ", "(n˘v˘•)¬", "♥╣[-_-]╠♥"
    , "★~(◡﹏◕✿)", "(◕‿-)", "( ^▽^)σ)~O~)"
    , "♥‿♥", "(✿ ♥‿♥)", "(●´ω｀●)"};
    static string[] partyFaces =
    { "♪└|∵|┐♪└|∵|┘♪┌|∵|┘♪ ♪└|∵┌|└| ∵ |┘|┐∵|┘", "ヾ(〃^∇^)ﾉ", "(ﾉ◕ヮ◕)ﾉ*:･ﾟ✧"
    , "♨(⋆‿⋆)♨", "┌( ಠ_ಠ)┘", "Ｏ(≧▽≦)Ｏ"
    , "☜-(ΘLΘ)-☞", "@(ᵕ.ᵕ)@", "╘[◉﹃◉]╕"};
    static string[] weirdFaces =
    { "（ ´_⊃｀）", "(￣。￣)～ｚｚｚ", "~(⊕⌢⊕)~"
    , "⊂•⊃_⊂•⊃", "ᕙ(⇀‸↼‶)ᕗ", "( 　ﾟ,_ゝﾟ)"
    , "(⊙︿⊙✿)", "̿̿<【☯】‿【☯】>̿", "( ͡° ͜ʖ ͡°)"};





    public static string GetFace(faceType type, int amount) {

        string put = "";

        for (int i = 0; i < amount; i++) {
            int x = Random.Range(0, 9);

            switch (type) {

                case faceType.Happy:
                    put += happyFaces[x];
                    break;
                case faceType.Sad:
                    put += sadFaces[x];
                    break;
                case faceType.Mad:
                    put += madFaces[x];
                    break;
                case faceType.Love:
                    put += loveFaces[x];
                    break;
                case faceType.Party:
                    put += partyFaces[x];
                    break;
                case faceType.Weird:
                    put += weirdFaces[x];
                    break;
                default:
                    break;
            }
        }

        return put;
    }




    /*
    static string[] sadFaces =
    { "xxxxxx", "xxxxxx", "xxxxxx"
    , "xxxxxx", "xxxxxx", "xxxxxx"
    , "xxxxxx", "xxxxxx", "xxxxxx"};
    */
}
