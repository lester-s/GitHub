   M S B A M L   `   `   `  ����  ChatApi.Client    ChatApi.Client.ChatPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  ChatPage��5       � 	Resources 5
         	itemstyle �� 
TargetType��%E   &	        �� 5   
   " ��6"   .�5      -   5        5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��3* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 	UsersList��5      
���  ��$��Stretch=�6&   $��0��6D   $��2��5      #��[  5      ��5      �� *Users+��5      �� 5      .R�5      �� ��6    �� 5       5"      �� 5$   
   -   5         ��Name$ ChatContainer��5$      $��1��6+   $��0��6;   $��Stretch=�6H   
��� � �6d   �� 5%   
   $��1��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��Stretch=�6K   $	��500��6i   .+�5&      �� -   5         ��Name$ 	TypingBox��5&      $��Stretch=�6)   
����  �$��10q�6Z   $	��300��6o   �� 5'      -   5        $
��Send��5'   )   $	��200��68   5(      5)      5*      