// (c) Copyright Evolite Studio. All rights reserved.
//Website: https://www.evolite-studio.com
//Twitter: @EvoliteStudio

using UnityEngine;
using UnityEditor;
using TMPro;
using System;

#if UNITY_EDITOR
[ExecuteInEditMode]

public class ArabicWriter : EditorWindow
{
    private string InputText;
    private string OutputText;
    public string[] _Slots =new string[7];
    public string[] _SlotsName = new string[]{"Slot 1:","Slot 2:","Slot 3","Slot 4:","Slot 5:","Slot 6:","Slot 7:"};
    public static Vector2 scrollPosition;
    public bool EasternArabicNumbers;
    private bool Tashkeel = true;
    private bool AutoCopyMode;
    private bool UseRTL;
    private bool FocusMode;
    private bool _Foldout;
    private bool _Foldout2;
    public int index = 0;
    public int NumOfTabs = 0;
    public int NumOfTabs2 = 0;
    public string[] Tabs = new string[] {"Arabic Writer", "Clipboard"};
    public string[] Tabs2 = new string[] {"Extras", "Info"};
    public string[] options = new string[] {"Drag and drop", "By selecting"};
    public GameObject TextComponent;
    
    

    [MenuItem("Tools/Arabic Writer/Arabic Writer window")]
    public static void ShowWindow ()
    {
      ArabicWriter window = EditorWindow.GetWindow<ArabicWriter>();
      Texture icon = AssetDatabase.LoadAssetAtPath<Texture> ("Assets/ArabicWriter/Icon/LOGO.png");
      window.minSize = new Vector2 (250,210);
      GUIContent titleContent = new GUIContent ("Arabic Writer", icon);
      window.titleContent = titleContent;
    }
    void OnGUI(){
      
      GUILayout.BeginHorizontal("toolbar");
      NumOfTabs = GUILayout.Toolbar(NumOfTabs, Tabs, "toolbarbutton");
      FocusMode = GUILayout.Toggle(FocusMode,new GUIContent(EditorGUIUtility.IconContent("scenevis_hidden_hover@2x").image,"Focus mode"),"toolbarButton",GUILayout.Width(40));
      if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("Favorite@2x").image,"Save to clipboard"), "toolbarDropDown", GUILayout.Width(40)))
        {
            GenericMenu SaveMenu = new GenericMenu();

            SaveMenu.AddItem(new GUIContent("Slot 1"), false, slot1);
            SaveMenu.AddItem(new GUIContent("Slot 2"), false, slot2);
            SaveMenu.AddItem(new GUIContent("Slot 3"), false, slot3);
            SaveMenu.AddItem(new GUIContent("Slot 4"), false, slot4);
            SaveMenu.AddItem(new GUIContent("Slot 5"), false, slot5);
            SaveMenu.AddItem(new GUIContent("Slot 6"), false, slot6);
            SaveMenu.AddItem(new GUIContent("Slot 7"), false, slot7);
            SaveMenu.ShowAsContext();
        }
      GUILayout.EndHorizontal();
      scrollPosition = GUILayout.BeginScrollView(scrollPosition);
      EditorGUILayout.BeginHorizontal();
      if  (position.width >= 600)
      {
        EditorGUILayout.BeginVertical("box",GUILayout.Width(position.width/3.5f),GUILayout.ExpandHeight(true));


EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
      GUILayout.BeginHorizontal();
      NumOfTabs2 = GUILayout.Toolbar(NumOfTabs2,Tabs2,"Toolbarbutton");
      
      
      GUILayout.EndHorizontal();
EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);       
switch (NumOfTabs2)
      {
        
        case 0:
        Tab_Text();
        break;

        case 1:
        Tab_Info();
        break;

      }

        EditorGUILayout.EndVertical();
      }
        switch (NumOfTabs)
      {
        
        case 0:
        Tab1();
        break;

        case 1:
        Tab2();
        break;

      }



      EditorGUILayout.EndHorizontal();
      
      GUILayout.EndScrollView();
      if (FocusMode != true)
      {
      GUI.skin.label.alignment = TextAnchor.MiddleLeft;
      GUI.backgroundColor = Color.white;
       GUILayout.BeginHorizontal("toolbar");

       if (GUILayout.Button(new GUIContent(EditorGUIUtility.IconContent("Linked@2x").image,"Links"), "toolbarButton", GUILayout.Width(40)))
        {
            GenericMenu LinksMenu = new GenericMenu();

            LinksMenu.AddItem(new GUIContent("• Twitter"), false, Link1);
            LinksMenu.AddItem(new GUIContent("• Facebook"), false, Link2);
            LinksMenu.AddItem(new GUIContent("• Instagram"), false, Link3);
            LinksMenu.AddItem(new GUIContent("• YouTube"), false, Link7);
            LinksMenu.AddItem(new GUIContent("• Linked in"), false, Link4);
            LinksMenu.AddItem(new GUIContent(""), false, slot4);
            LinksMenu.AddItem(new GUIContent("★★ Rate us ★★"), false, Link5);
            LinksMenu.AddItem(new GUIContent(""), false, slot4);
            LinksMenu.AddItem(new GUIContent("Email us ✉"), false, Link6);

            LinksMenu.ShowAsContext();
        }
      
      GUI.skin.label.alignment = TextAnchor.MiddleRight;
       GUILayout.Label("v1.1");

      GUILayout.EndHorizontal();
      }
      }

       public void Update()
      {
        if (OutputText != InputText)
        {
          OutputText = ArabicSupport.Fix(InputText, Tashkeel, EasternArabicNumbers);
          if (UseRTL == true){
          OutputText = Reverse(OutputText);
          }
        }
      if(TextComponent != null) {

        if (TextComponent.GetComponent<TextMeshProUGUI>()!= null)
        {
          TextComponent.GetComponent<TextMeshProUGUI>().text = OutputText;
        }

        if (TextComponent.GetComponent<TextMeshPro>()!= null)
        {
          TextComponent.GetComponent<TextMeshPro>().text = OutputText;  
        }
      }   
      }
    private void DoSwitch()
      {
        switch (index)
        {
          case 1:
          TextComponent = Selection.activeGameObject;
          break;
          
          case 0:
          TextComponent = EditorGUILayout.ObjectField(TextComponent, typeof(GameObject), true) as GameObject; 
          break;
        }
      }
      private string Reverse(string outputText)
    {string r = "";
      if (outputText != null){      
        
        for(int i = OutputText.Length -1; i >= 0; i--){
          r += OutputText[i];
        }
       
    }
     return r;
    }  
    private void Tab1()
    {
      EditorGUILayout.BeginVertical();
        EditorStyles.textField.wordWrap = true;

       //GUI.skin.label.alignment = TextAnchor.UpperRight;
        GUI.backgroundColor = Color.grey;
        GUILayout.BeginVertical(EditorStyles.centeredGreyMiniLabel);
        GUILayout.Label("Type here:", "boldLabel");
        InputText = EditorGUILayout.TextArea(InputText,GUILayout.Height(position.height / 5.9f));
        GUILayout.Label("Correct text:", EditorStyles.boldLabel);
        GUILayout.Label(OutputText,"TextField",GUILayout.Height(position.height / 5.9f));
        GUILayout.EndVertical();
        GUI.backgroundColor = Color.white;
        GUI.color = Color.white;
       // EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.BeginHorizontal("box");
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button(new GUIContent("Copy","Copy the correct text"), "ButtonLeft"))
        {
          GUIUtility.systemCopyBuffer = OutputText;
        }
        GUI.backgroundColor = Color.red;
       if( GUILayout.Button(new GUIContent("Delete","Erase the written"),"ButtonRight")){
        GUI.FocusControl(null);
        InputText = "";
        OutputText = "";}
       
        GUILayout.EndHorizontal();
        if (FocusMode != true)
        {
        GUI.backgroundColor = Color.white;
        GUILayout.BeginHorizontal("box");
        EasternArabicNumbers = EditorGUILayout.Toggle("Eastern Arabic Numbers",EasternArabicNumbers);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("box");
        Tashkeel = EditorGUILayout.Toggle("Show Diacritics",Tashkeel);
        GUILayout.EndHorizontal();
        GUI.color = Color.white;
        GUILayout.BeginHorizontal("box");
        UseRTL = EditorGUILayout.Toggle("Use RTL",UseRTL);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal("box");
        AutoCopyMode = EditorGUILayout.Toggle("Auto copy & paste mode",AutoCopyMode);
        GUILayout.EndHorizontal();
        if (AutoCopyMode == true)
        {
          EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
          GUILayout.Label("Choose the Text selection method", EditorStyles.largeLabel);
          GUILayout.BeginHorizontal("box");
          index = EditorGUILayout.Popup(index, options);
          DoSwitch();
          GUILayout.EndHorizontal();
        if (TextComponent == null)
          {
            EditorGUILayout.HelpBox("There is no text to edit",MessageType.Warning);
          }
          else{
            
            if (GUILayout.Button("Finish"))
        {
          TextComponent = null;
          Selection.activeGameObject = null;
          InputText = null;
          OutputText = null;
        }
       if (TextComponent != null){ 
        if (TextComponent.GetComponent<TextMeshProUGUI>() == null)
         if (TextComponent.GetComponent<TextMeshPro>() == null ){
          EditorGUILayout.HelpBox("The selected GameObject doesn't have any TMP-Text component",MessageType.Error);
          }
          else{}
       }
        
          }
          GUI.color = Color.white;
        }
        else
        {
          TextComponent = null;
        }  
        }
        GUI.backgroundColor = Color.white;
        EditorGUILayout.EndVertical();
       
    }
    private void Tab2()
    {
      GUILayout.BeginVertical(EditorStyles.centeredGreyMiniLabel);
      GUIContent TrashIcon = EditorGUIUtility.IconContent("d_winbtn_win_close");
      GUIContent SaveIcon = EditorGUIUtility.IconContent("d_SaveAs");
      GUI.skin.label.alignment = TextAnchor.UpperLeft;
      GUI.backgroundColor = Color.grey;

      for (var i = 0; i < _Slots.Length; i++)
      {
        GUILayout.Label(_SlotsName[i], "boldLabel");
      GUILayout.BeginHorizontal("box");
      GUILayout.Label(_Slots[i],"TextField");
      if (GUILayout.Button(SaveIcon,"ButtonLeft",GUILayout.Width(40)))
        {
          GUIUtility.systemCopyBuffer = _Slots[i];
        }
      if (GUILayout.Button(TrashIcon,"ButtonRight",GUILayout.Width(40)))
        {
          _Slots[i] = "";
        }
      GUILayout.EndHorizontal();}

      EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
      GUI.backgroundColor = Color.red;
       if (GUILayout.Button("Clear all","LargeButton"))
        {
         _Slots = new string[]{"","","","","","",""};
        }
      GUI.backgroundColor = Color.white;  
      GUILayout.EndVertical();
      
    }  

    private void slot1()
    {
       _Slots[0] = OutputText;
    }
    private void slot2()
    {
       _Slots[1] = OutputText;
    }
    private void slot3()
    {
       _Slots[2] = OutputText;
    }
    private void slot4()
    {
       _Slots[3] = OutputText;
    }
    private void slot5()
    {
       _Slots[4] = OutputText;
    }
     private void slot6()
    {
       _Slots[5] = OutputText;
    }
     private void slot7()
    {
       _Slots[6] = OutputText;
    }
    private void Link1()
    {
      Application.OpenURL("https://twitter.com/EvoliteStudio");
    }
    private void Link2()
    {
      Application.OpenURL("https://www.facebook.com/EvoliteStudio");
    }
    private void Link3()
    {
      Application.OpenURL("https://www.instagram.com/evolitestudio");
    }
    private void Link4()
    {
      Application.OpenURL("https://www.linkedin.com/company/evolite-studio");
    }
    private void Link5()
    {
      Application.OpenURL("https://assetstore.unity.com/packages/slug/234295");
    }
    private void Link6()
    {
      Application.OpenURL("mailto:contact@evolite-studio.com");
    }
    private void Link7()
    {
      Application.OpenURL("https://www.youtube.com/@EvoliteStudio");
    }
    private void Tab_Text()
    {
      
      GUILayout.Label("Click on a symbol to copy", EditorStyles.largeLabel);
      GUILayout.BeginHorizontal("box");
      if (GUILayout.Button(new GUIContent("•","Bullet"),"ButtonLeft"))
        {
          GUIUtility.systemCopyBuffer = "•";
        }
      if (GUILayout.Button(new GUIContent("▪","Black Small Square"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "▪";
        }
      if (GUILayout.Button(new GUIContent("°","Degree Sign"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "°";
        }
      if (GUILayout.Button(new GUIContent("←","Leftwards Arrow"),"ButtonRight",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "←";
        }    
      GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal("box");
      if (GUILayout.Button(new GUIContent("©","Copyright Sign"),"ButtonLeft"))
        {
          GUIUtility.systemCopyBuffer = "©";
        }
      if (GUILayout.Button(new GUIContent("®","Registered Sign"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "®";
        }
      if (GUILayout.Button(new GUIContent("℗","Sound Recording Copyright"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "℗";
        }
      if (GUILayout.Button(new GUIContent("™","Trade Mark Sign"),"ButtonRight",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "™";
        }    
      GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal("box");
      if (GUILayout.Button(new GUIContent("$","Dollar Sign"),"ButtonLeft"))
        {
          GUIUtility.systemCopyBuffer = "$";
        }
      if (GUILayout.Button(new GUIContent("€","Euro Sign"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "€";
        }
      if (GUILayout.Button(new GUIContent("¥"," Yen Sign"),"ButtonMid",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "¥";
        }
      if (GUILayout.Button(new GUIContent("﷼","Rial Sign"),"ButtonRight",GUILayout.Width(50)))
        {
          GUIUtility.systemCopyBuffer = "﷼";
        }    
      GUILayout.EndHorizontal();    
      EditorGUILayout.Space(15); 

      
      GUILayout.Label("Arabic HEX:", EditorStyles.largeLabel);
      GUILayout.Label("0600-06FF,0750-077F,08A0-08FF,FB50-FDFF,FE70-FEFF,10E60-10E7F,1EE00-1EEFF","TextField",GUILayout.Height(position.height / 5.9f));
      if (GUILayout.Button("Copy"))
        {
          GUIUtility.systemCopyBuffer = "0600-06FF,0750-077F,08A0-08FF,FB50-FDFF,FE70-FEFF,10E60-10E7F,1EE00-1EEFF";
        }
    }
    private void Tab_Info()
    {
      GUI.skin.label.alignment = TextAnchor.UpperLeft;
      EditorStyles.label.wordWrap = true;
      GUILayout.Label("ABOUT THIS TOOL", EditorStyles.largeLabel);
      GUILayout.Label("Arabic Writer is a lightweight asset allows you you to write in Arabic correctly and quickly without connecting to the Internet!",EditorStyles.label);
      EditorGUILayout.Space(15); 
      GUILayout.Label("CONTACT US", EditorStyles.largeLabel);
      GUILayout.Label("We are working hard to develop this tool and we are interested to hear your opinions",EditorStyles.label);
      EditorGUILayout.Space(10); 
      if (GUILayout.Button("Contact us"))
        {
            GenericMenu LinksMenu = new GenericMenu();

            LinksMenu.AddItem(new GUIContent("• Twitter"), false, Link1);
            LinksMenu.AddItem(new GUIContent("• Facebook"), false, Link2);
            LinksMenu.AddItem(new GUIContent("• Instagram"), false, Link3);
            LinksMenu.AddItem(new GUIContent("• YouTube"), false, Link7);
            LinksMenu.AddItem(new GUIContent("• Linked in"), false, Link4);
            LinksMenu.AddItem(new GUIContent(""), false, slot4);
            LinksMenu.AddItem(new GUIContent("★★ Rate us ★★"), false, Link5);
            LinksMenu.AddItem(new GUIContent(""), false, slot4);
            LinksMenu.AddItem(new GUIContent("Email us ✉"), false, Link6);

            LinksMenu.ShowAsContext();
        }
      EditorGUILayout.Space(15);
      _Foldout2 = EditorGUILayout.Foldout(_Foldout2,"WHO ARE WE",EditorStyles.foldoutHeader);
      if (_Foldout2)
      {
        GUILayout.Label("An indie studio specializing in software development & game making | Since 2016",EditorStyles.label);
      } 
      _Foldout = EditorGUILayout.Foldout(_Foldout,"SPECIAL THANKS LIST",EditorStyles.foldoutHeader);
      if (_Foldout)
      {
        GUILayout.Label("@ArabGameAwards\n@DigitaleAnime\n@Konash\n@_Nine66_\n@JustPlayIt_");
      }
      

}
}

#endif
