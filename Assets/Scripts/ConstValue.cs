﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//id
public enum EntityID
{
	GUIManager = 0,
	GameManager,
}

public enum GameDoneState
{
	GameWin = 0,
	GameOver,
	GamePause
}

public enum GamePropsId {
	None = 0,
	Bomb,
	Rocket,
	Shock,
	Same
}

public class ConstValue {
	public const string RES_GUI_PATH	= "GUIPrefabs";
	public const string GUI_LEVEL		= "Panel Level";
	public const string GUI_LOAD		= "Panel Load";
	public const string GUI_WIN			= "Panel Win";
	public const string GUI_OVER		= "Panel Over";
	public const string GUI_PLAYING		= "Panel Playing";

	public const string RES_GAME_PATH	= "Prefabs";
	public const string GAME_LIAN_PRE	= "lian_";
	public const string GAME_LINE_HEN	= "heng_line";
	public const string GAME_LINE_SHU	= "shu_line";
	public const string GAME_BACKGROUND	= "background";
	public const string GAME_B_BANNER	= "BottomBanner";
	public const string GAME_T_BANNER	= "Top Banner";
	public const string GAME_BOSS_FIRST	= "Boss First";

	public const string RES_PROP_PATH	= RES_GAME_PATH + "/Props";
	public const string GAME_PROP_BOMB	= "Prop Bomb";
	public const string GAME_PROP_ROCKET= "Prop Rocket";
	public const string GAME_PROP_SHOCK	= "Prop Shock";
	public const string GAME_PROP_SAME	= "Prop Same";

	public const string RES_PART_PATH	= "Particles";
	public const string GAME_BOX_EXP	= "Particle Box Exp";
	public const string GAME_BOMB_EXP	= "Particle Bomb Exp";
	public const string GAME_ROCKET_TR	= "Prop Rocket Trail";

	public const string PRE_CUR_LEVEL	= "Current Level";

	public const string MSG_START_GAME		= "Start Game";
	public const string MSG_GAME_DONE		= "Game Done";
	public const string MSG_GAME_TO_LEVEL	= "Game To Level";
	public const string MSG_USE_PROP		= "Use Prop";
	public const string MSG_USE_PROP_SUC	= "Use Prop Suc";
	public const string MSG_USE_PROP_CEL	= "Use Prop Cancel";

	public const float BOTTOM_BAN_HEIGHT	= 1f;

	public static float		BoxWidth;
	public static float		BoxHeight;
	public static float		TopBannerHeight;
	public static float		BossHeight;

	public static int		level;
	public static Level		currentLevel;

	public static Dictionary<int, string> bosses = new Dictionary<int, string>()
	{
		{1, ConstValue.GAME_BOSS_FIRST}
	};
}
