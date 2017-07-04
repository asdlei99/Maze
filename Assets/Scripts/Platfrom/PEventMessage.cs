using UnityEngine;
using System.Collections;

public enum PEventMessage : byte
{
	Login				= 1,	//登陆;
	Payment				= 2,	//支付;
	UserCenter			= 3,	//用户中心;
	LoginOut			= 4,	//退出;
	BindAccount			= 5,	//绑定账号;
	SwitchAccount		= 6,	//切换账号;
	InviteFriend		= 7,	//邀请好友;
	EnterWorld			= 8,	//进入世界地图;
	ExitWorld			= 9,	//退出世界地图;
	ContactUs			= 10,	//联系客服;
	Match				= 11,	//赛事;
	Announcement		= 12,	//公告;
	IntegralWall		= 13,	//积分墙;
	AntiAddiction		= 14,	//防沉迷;
	VIP					= 15,	//VIP;
	UploadPhotoNotice	= 16,	//上传头像通知;
	BindUser			= 17,	//游客绑定;

	PokerVisitorLogin	= 100,	//游客绑定;
}