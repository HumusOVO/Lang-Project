1. 开始游戏状态 
   点击开始后自动进行一次方向向下的主盘移动状态

2. 主盘移动状态
   主盘移动，生成方块并存储，确定左右上下边界，方块，方块前进方向，已在主盘内的方块移动方向

3. 方块运动阶段（正常游戏）
   进行正常运动

4.判断方块消除
  判断本列方块是否到达规定数量，到达则消除

4.游戏结束阶段
   方块无法放下/方块超出上边界，游戏结束

各类会改变的参数，方向（方向的改变对整体起作用）放在GameManager中