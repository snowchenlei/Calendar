using System.Globalization;
using Snow.Calendar.Common.Model;

namespace Snow.Calendar.Common
{
    /// <summary>
    /// 星宿
    /// </summary>
    public class Constellation
    {
        private const string Animals = "鼠牛虎兔龙蛇马羊猴鸡狗猪";

        /// <summary>
        /// 星宿
        /// </summary>
        private ConstellationModel[,] Starts =
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
        /// 星座
        /// </summary>
        private static readonly string[] Constellations = new[]
        {
            "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座",
            "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座"
        };

        /// <summary>
        /// 诞生石
        /// </summary>
        private static readonly string[] BirthStones = new[]
        {
            "钻石", "蓝宝石", "玛瑙", "珍珠", "红宝石", "红条纹玛瑙",
            "蓝宝石", "猫眼石", "黄宝石", "土耳其玉", "紫水晶", "月长石，血石"
        };

        /// <summary>
        /// 星宫
        /// </summary>
        private static readonly string[] Palaces = new[]
        {
            "始宫", "续宫", "果宫"
        };

        /// <summary>
        /// 行星
        /// </summary>
        private static readonly string[] Planets = new[]
        {
            "火星", "金星", "水星", "月亮", "太阳", "水星",
            "金星", "火星", "木星", "土星", "天王星&土星", "木星&海王星"
        };

        private readonly ChineseLunisolarCalendar _chineseLunisolarCalendar;

        public Constellation(
            ChineseLunisolarCalendar chineseLunisolarCalendar)
        {
            _chineseLunisolarCalendar = chineseLunisolarCalendar;
        }

        /// <summary>
        /// 阴历年生肖
        /// </summary>
        public string GetLunarYearAnimal(DateTime time)
        {
            int y = _chineseLunisolarCalendar.GetSexagenaryYear(time);
            return Animals.Substring((y - 1) % 12, 1);
        }

        /// <summary>
        /// 获取星宿描述
        /// </summary>
        /// <param name="month">农历月</param>
        /// <param name="day">农历日</param>
        /// <returns>星宿信息</returns>
        public ConstellationModel GetStart(int month, int day)
        {
            return Starts[month, day];
        }

        /// <summary>
        /// 根据指定阳历日期计算星座＆诞生石
        /// </summary>
        /// <param name="date">指定阳历日期</param>
        /// <param name="constellation">星座</param>
        /// <param name="birthstone">诞生石</param>
        public ConstellationInfo GetConstellation(DateTime date)
        {
            int i = Convert.ToInt32(date.ToString("MMdd"));
            int j;
            if (i >= 321 && i <= 419)
                j = 0;
            else if (i >= 420 && i <= 520)
                j = 1;
            else if (i >= 521 && i <= 621)
                j = 2;
            else if (i >= 622 && i <= 722)
                j = 3;
            else if (i >= 723 && i <= 822)
                j = 4;
            else if (i >= 823 && i <= 922)
                j = 5;
            else if (i >= 923 && i <= 1023)
                j = 6;
            else if (i >= 1024 && i <= 1121)
                j = 7;
            else if (i >= 1122 && i <= 1221)
                j = 8;
            else if (i >= 1222 || i <= 119)
                j = 9;
            else if (i >= 120 && i <= 218)
                j = 10;
            else if (i >= 219 && i <= 320)
                j = 11;
            else
            {
                return new ConstellationInfo();
            }

            return new ConstellationInfo()
            {
                SolarConstellation = Constellations[j],
                SolarBirthStone = BirthStones[j],
                SolarPalace = Palaces[j % 3],
                SolarPlanet = Planets[j]
            };

            #region 星座划分

            //白羊座：   3月21日------4月19日     诞生石：   钻石
            //金牛座：   4月20日------5月20日   诞生石：   蓝宝石
            //双子座：   5月21日------6月21日     诞生石：   玛瑙
            //巨蟹座：   6月22日------7月22日   诞生石：   珍珠
            //狮子座：   7月23日------8月22日   诞生石：   红宝石
            //处女座：   8月23日------9月22日   诞生石：   红条纹玛瑙
            //天秤座：   9月23日------10月23日     诞生石：   蓝宝石
            //天蝎座：   10月24日-----11月21日     诞生石：   猫眼石
            //射手座：   11月22日-----12月21日   诞生石：   黄宝石
            //摩羯座：   12月22日-----1月19日   诞生石：   土耳其玉
            //水瓶座：   1月20日-----2月18日   诞生石：   紫水晶
            //双鱼座：   2月19日------3月20日   诞生石：   月长石，血石

            #endregion 星座划分
        }
    }
}