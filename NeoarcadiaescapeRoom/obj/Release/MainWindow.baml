   M S B A M L   `   `   `  ���� D  @ColorFont, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:ColorFont;assembly=ColorFont	ColorFont   NeoarcadiaescapeRoom%   NeoarcadiaescapeRoom.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  6local*clr-namespace:ColorFont;assembly=ColorFont  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  
MainWindow��.���  � ColumnDefinitions �� $��* ��� $
��1.5* �.+�� $��0�� � RowDefinitions �� $��* ��� $	��20* �.+�]� $��0��.+�o� $	��Top=����	#FFF0F0F0.@�m� $
��File��.@�m� -    m�Name$
 exit��$
��Exit��m� $��Settings��.@�m� -   $ newSettings��$	��New��m� -   $ editSettings��$
��Edit��� $��1�����	#FFF0F0F0 �� $��0.75* ��� $��5* �.+�� $��0�� �� $��* ��� $��* ��� $��* � �� $
��0.5* ��� $
��0.5* �.+��� -    ��Name$ NewGame��$��0��$��0��$��NewGame��$��10q��� -   $ StopGame��$��1��$��0��{�.  $��StopGame��$��10q��� -   $ 	PauseGame��$��2��$��0��{�.  $��	PauseGame��$��10q��� -   $ 	addMinute��$��1��$��1��{�.  $
��+min��$��10q��� -	   $ removeMinute��$��2��$��1��{�.  $
��-min��$��10q�%  Xceed.Wpf.Toolkit.IntegerUpDown  -
     Name$ minToAdd��$��0��$��1���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   DefaultValue$ 32X�$��5,5,5,0q�%   DisplayDefaultValueOnEmptyText .    Value$ 1X�� $��1�� �� $��0.75* ��� $��* ��� $
��1.5* ��� $
��0.5* ��� $
��0.5* ��� $��0.75* ��� $��* ��� $��* �.+��� $��0��$
��Clue��$��Center=�$��21(��� -   	 ��Name$	 Question��"
 �� VerticalScrollBarVisibility$

 Auto=�$ �� HorizontalScrollBarVisibility$
 Auto=� �� AcceptsReturn . $��1��$��10q�
�������{�.  � $��2�� �� $
��1.5* ��� $��2* �.+��� $��0��$��Clue Pictures��$��Center=�$��21(�� $��1�� �� $��2* ��� $��* �.+��� -    ��Name$ CluePictures��$��Visible=�$��0��$��10q�
�������{�.  � $��1�� �� $��* ��� $��* ��� $��* �.+��� -   $ AddCluePicture��$��0��{�.  $��Add pct Clue���� -   $ RemoveCluePicture��$��1��{�.  $��Remove pct Clue���� -   $ ClearCluePicture��$��2��{�.  $��Clear pct Clue���� -    ��Name$ RemovePointCheckBox��$��Center=�{�.  $��3��.��	-2 Points� $��4�� �� $��* ��� $��* �.+��� -   $
 Send��$��0��$
��Send��$��10q�{�.  �� -   $ Delete��$��1��$��Delete��$��10q�{�.  �� $��5��$��Scoring��$��Center=�$��21(�� -    �Name$ ScoringPannel��$��6�� �� $��* ��� $��* � �� $��0.25* ��� $��0.25* ��� $��0.25* ��� $��0.25* �.+��� -   $��0��
 � Tag$ 1��$��0��{�.  $��+1��$��10,10,10,10q��� -   $��1��$ 2��$��0��{�.  $��+2��$��10,10,10,10q��� -   $��2��$ 10��$��0��{�.  $	��+10��$��10,10,10,10q��� -   $��0��$ -1��$��1��{�.  $��-1��$��10,10,10,10q��� -   $��1��$ -2��$��1��{�.  $��-2��$��10,10,10,10q��� -   $��2��$	 -10��$��1��{�.  $	��-10��$��10,10,10,10q�  -   $��DefaultScore��$��3��$��0��$	 100X� . $	 100X�$��10,10,10,10q��� -   $��3��$��1��{�.  $��Victoire��$��10,10,10,10q�� $��7�� �� $��* ��� $��* �.+��� -    ��Name$ MTimer��$��0��$
��Time��$��Center=��� -   $ MScore��$��1��$��Score��$��Center=�� $��1�� �� $
��0.5* ��� $��4* �.+��� $��Params��$��0��$��Center=�$��32(�� $��1�� �� $��* ��� $��3* � �� $
��2.5* ��� $��* ��� $��* ��� $��* �.+��� $��0��$��0��.+��� $��Config Font���� $��Background color���� $��Background image���� $��Use background image���� $��Current Clue���� $��	Clue Text���� $��Auto Remove Clue���� $��Game Over Screen���� $��Background music���� $�� ���� $�� ���� $��0��$��2��.+��� $	��SFX���� $��1��$��0��.+��� $��HUD font size���� $��Timer font size���� $��Clue font size���� $��Score font size���� $��0��$��3��.+��� $��
Clue Sound���� $��0��$��1��.+��� -   $ 
fontConfig��{�.  $��Config font��$��2,5,2,2q�   ColorFont.ColorPicker  -     Name$ colorPicker��{�.  $��20��$
��Auto��$��2,5,2,2q��  �� $��* ��� $��* �.+��� -    $ BgimageLabel��$��0��$�� ���� -!   $ browseBgImg��{�.  $��1��$��2,5,2,2q�$��Change Background Image���� -"   $ checkUseBgImg��{�.  $��Use background image���� -#   $ CurrentClueLabel��$��5,02,5,0q�$��25������ *CurrentClue+�� -$   $ LastClueLabel��$��5,0,5,0q�$��25������ *
LastClue+�  �� $��* ��� $��* ��� $��* �.+�  -%   $ 
deleteTime��{�.  $��0��   Minimum$ 0X�$ 10X�$��5,5,5,0q� . �� -&   $ 
deleteAuto��{�.  $��2��$��5,10,5,0q��� $��1��$��Enabled���  �� $��* ��� $��* �.+��� -'   $ GameOverScreenLabel��$��0���� -(   $ GameOverScreenChanged��$��1��$��browse��{�.  �� -)   $ BackgroundMusicLabel��$��0��$�� ��$��5,0,5,0q�$��25���  �� $��* ��� $��* ��� $��* ��� $��* �.+��� $��0��$
��Loop���� -*   $ LoopBgdM��$��1��{�.  $��5,5,5,0q��� -+   $ BrowseBgdMusic��$��2��{�.  $
��Play��$��5,0,5,0q��� -,   $ StopBgdMusic��$��3��{�.  $
��Stop��$��5,0,5,0q��� $��1��$��1��.+�  --   $ HubFontSizeControl��$ 32X�$��5,5,5,0q� .  �� *HubFontSize+  -.   $ TimerFontSizeControl��$	 120X�$��5,5,5,0q� .  �� *TimerFontSize+  -/   $ ClueFontSizeControl��$	 120X�$��5,5,5,0q� .  �� *ClueFontSize+  -0   $ ScoreFontSizeControl��$	 120X�$��5,5,5,0q� .  �� *ScoreFontSize+�� $��1��$��2��$��1��$��Disabled=�$
��Auto=�.��� -1    �Name$ sfxPanel�� � Orientation$ 
Horizontal=��� -2   $ 	SoundList��$��1��$��3��$��5q�$��Visible=� �� SelectedValue �� *SelectedSound+