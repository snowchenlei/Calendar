﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snow.Calendar.Web.Model;

namespace Snow.Calendar.Web.Common
{
    /// <summary>
    /// 星宿
    /// </summary>
    public class Constellation
    {
        private static string[] ConstellationName =
        {
            //四      五        六       日        一       二        三
            "角木蛟", "亢金龙", "氐土貉", "房日兔", "心月狐", "尾火虎", "箕水豹",
            "斗木獬", "牛金牛", "女土蝠", "虚日鼠", "危月燕", "室火猪", "壁水獝",
            "奎木狼", "娄金狗", "胃土彘", "昴日鸡", "毕月乌", "觜火猴", "参水猿",
            "井木犴", "鬼金羊", "柳土獐", "星日马", "张月鹿", "翼火蛇", "轸水蚓"
        };

        private ConstellationModel[,] Constellations =
        {
            {
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"}
            },
            {
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"}
            },
            {
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
            },
            {
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
            },
            {
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
            },
            {
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
            },
            {
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
            },
            {
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
            },
            {
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
            },
            {
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
            },
            {
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
            },
            {
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
                new ConstellationModel(){ConstellationName = "壁宿", ConstellationValue = "壁水獝"},
                new ConstellationModel(){ConstellationName = "奎宿", ConstellationValue = "奎木狼"},
                new ConstellationModel(){ConstellationName = "娄宿", ConstellationValue = "娄金狗"},
                new ConstellationModel(){ConstellationName = "胃宿", ConstellationValue = "胃土彘"},
                new ConstellationModel(){ConstellationName = "昴宿", ConstellationValue = "昴日鸡"},
                new ConstellationModel(){ConstellationName = "毕宿", ConstellationValue = "毕月乌"},
                new ConstellationModel(){ConstellationName = "觜宿", ConstellationValue = "觜火猴"},
                new ConstellationModel(){ConstellationName = "参宿", ConstellationValue = "参水猿"},
                new ConstellationModel(){ConstellationName = "井宿", ConstellationValue = "井木犴"},
                new ConstellationModel(){ConstellationName = "鬼宿", ConstellationValue = "鬼金羊"},
                new ConstellationModel(){ConstellationName = "柳宿", ConstellationValue = "柳土獐"},
                new ConstellationModel(){ConstellationName = "星宿", ConstellationValue = "星日马"},
                new ConstellationModel(){ConstellationName = "张宿", ConstellationValue = "张月鹿"},
                new ConstellationModel(){ConstellationName = "翼宿", ConstellationValue = "翼火蛇"},
                new ConstellationModel(){ConstellationName = "轸宿", ConstellationValue = "轸水蚓"},
                new ConstellationModel(){ConstellationName = "角宿", ConstellationValue = "角木蛟"},
                new ConstellationModel(){ConstellationName = "亢宿", ConstellationValue = "亢金龙"},
                new ConstellationModel(){ConstellationName = "氐宿", ConstellationValue = "氐土貉"},
                new ConstellationModel(){ConstellationName = "房宿", ConstellationValue = "房日兔"},
                new ConstellationModel(){ConstellationName = "心宿", ConstellationValue = "心月狐"},
                new ConstellationModel(){ConstellationName = "尾宿", ConstellationValue = "尾火虎"},
                new ConstellationModel(){ConstellationName = "箕宿", ConstellationValue = "箕水豹"},
                new ConstellationModel(){ConstellationName = "斗宿", ConstellationValue = "斗木獬"},
                new ConstellationModel(){ConstellationName = "女宿", ConstellationValue = "女土蝠"},
                new ConstellationModel(){ConstellationName = "虚宿", ConstellationValue = "虚日鼠"},
                new ConstellationModel(){ConstellationName = "危宿", ConstellationValue = "危月燕"},
                new ConstellationModel(){ConstellationName = "室宿", ConstellationValue = "室火猪"},
            },
        };

        /// <summary>
        /// 获取星宿描述
        /// </summary>
        /// <param name="month">农历月</param>
        /// <param name="day">农历日</param>
        /// <returns>星宿信息</returns>
        public ConstellationModel GetConstellation(int month, int day)
        {
            return Constellations[month, day];
        }
    }
}