   M S B A M L   `   `   `  ����F  BConnect4WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Connect4WPFConnect4WPF  5   	       Connect4WPF.mainMenu   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   %localclr-namespace:Connect4WPF  5   	     � Title$  Connect 4 - Main Menu��5	   	   $	��450��6'   $	��600��64   
�����_�.��5
      � $	��450��6   $	��600��6   
������ .+�5   
   >� .��5      � -   5        $��ContentGrid��5      $��0,0,0,29q�6&    � ColumnDefinitions 5      �� 5      $	��185 �6'   65   �� 5      $	��230 �6'   65   �� 5      $	��185 �6'   65    � RowDefinitions 5      �� 5      $��80 �6$   62   �� 5      $	��280 �6$   63   �� 5      $��80 �6$   62   .+�5      �� $��3��6   $��128,0,129,0q�6-   .+�5      A� -   5        $��YellowPiece��5      
 �� Top$ 15��61    �� Left$ 10��6A   $��50��6R   $��50��6^   ��5      
� 5      .��5   "   �  � Color$ 	#FF777C14��6/    � Offset$ 0Q�6A   � 5   "   $ 	#FFF7FF00��6/   $
 0.59Q�6A   5      5       �� 5!      -   5        $��	MainTitle��5!      "  WpfAnimatedGif.ImageBehavior   AnimatedSource$" Images/Connect4MainTitle.gif��6-      AnimateInDesignMode . $	��230��6�   $��50��6�   $ 60��6�   $ 15��6�   6�   A� 5"      -   5        $��RedPiece��5"      $ 15��6.   $	 290��6>   $��50��6P   $��50��6\   ��5#      
� 5$      .��5%   "   � $ 	#FF820000��6/   � 5&   "   $	 Red��6/   $
 0.59Q�6;   5'      5)      5*      �� 5+      -   5        $��btn_Play��5+      $��1��6)   $��1��69   $��46,43,46,188q�6F   $
��PLAY��6\   6�   �� 5,      -   5        $��btn_PlayerSettings��5,      $��1��63   $��1��6C   $��46,113,46,118q�6P   $��PLAYER SETTINGS��6g   6�   �� 5-      -   5        $��btn_GameSettings��5-      $��1��61   $��1��6A   $��46,183,46,48q�6N   $��GAME SETTINGS��6d   6�   5.      50      �� 51   
   -   5        $��GreyOverlay��51      $	��450��6'   $	��600��64   
���   �	 J� Opacity$		 0.7Q�6M   $y�Hidden=�6[   6q   >� 52   
   -	   5        $��playerSettingsUserControl��52      |�. .��54      � -
   5        $��playerSettingsGrid��54      $y�Hidden=�6-   |�.  55      �� 56      $	��150 �6'   65   �� 57      $	��300 �6'   65   �� 58      $	��150 �6'   65   .+�5;      �� -   5        $��tbx_player1Name��5;      $��1��61   $��0,142,170,285q�6A   
�������$��Yellow��6k   
 J� IsHitTestVisible
 . |�.  �� 
IsReadOnly .  
���   � �� 
CaretBrush
 �   �6  �� 5<      -   5        $��tbx_player2Name��5<      $��1��61   $��170,142,0,285q�6A   
�������$	��Red��6k   
 . |�.  .  
���   �6�   5>      5?      5A      5D      