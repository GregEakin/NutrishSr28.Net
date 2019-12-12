    create table ABBREV (
        NDB_No NVARCHAR(5) not null,
       primary key (NDB_No)
    )

    create table DERIV_CD (
        Deriv_Cd NVARCHAR(4) not null,
       Deriv_Desc NVARCHAR(120) not null,
       primary key (Deriv_Cd)
    )

    create table DATA_SRC (
        DataSrc_ID NVARCHAR(6) not null,
       Authors NVARCHAR(255) null,
       Title NVARCHAR(255) null,
       Year NVARCHAR(4) null,
       Journal NVARCHAR(135) null,
       Vol_City NVARCHAR(16) null,
       Issue_State NVARCHAR(5) null,
       Start_Page NVARCHAR(5) null,
       End_Page NVARCHAR(5) null,
       primary key (DataSrc_ID)
    )

    create table FOOD_DES (
        NDB_No NVARCHAR(5) not null,
       FdGrp_Cd NVARCHAR(4) not null,
       Long_Desc NVARCHAR(200) not null,
       Shrt_Desc NVARCHAR(60) not null,
       ComName NVARCHAR(100) null,
       ManufacName NVARCHAR(65) null,
       Survey NVARCHAR(1) null,
       Ref_desc NVARCHAR(135) null,
       Refuse INT null,
       SciName NVARCHAR(65) null,
       N_Factor FLOAT(53) null,
       Pro_Factor FLOAT(53) null,
       Fat_Factor FLOAT(53) null,
       CHO_Factor FLOAT(53) null,
       primary key (NDB_No)
    )

    create table FD_GROUP (
        FdGrp_Cd NVARCHAR(4) not null,
       FdGrp_Desc NVARCHAR(60) not null,
       foodGroup NVARCHAR(4) null,
       primary key (FdGrp_Cd)
    )

    create table FOOTNOTE (
        ID INT IDENTITY NOT NULL,
       NDB_No NVARCHAR(5) null,
       Footnt_No NVARCHAR(4) not null,
       Footnt_Typ NVARCHAR(1) not null,
       Nutr_No NVARCHAR(3) null,
       Footnt_Txt NVARCHAR(200) not null,
       primary key (ID)
    )

    create table LANGDESC (
        Factor_Code NVARCHAR(5) not null,
       Description NVARCHAR(140) not null,
       primary key (Factor_Code)
    )

    create table languageSet (
        languageSet NVARCHAR(5) not null,
       ITEM_ID NVARCHAR(5) not null,
       primary key (languageSet, ITEM_ID)
    )

    create table NUT_DATA (
        NDB_No NVARCHAR(5) not null,
       Nutr_No NVARCHAR(3) not null,
       Nutr_Val FLOAT(53) not null,
       Num_Data_Pts INT not null,
       Std_Error FLOAT(53) null,
       Src_Cd NVARCHAR(2) not null,
       Deriv_Cd NVARCHAR(4) null,
       Ref_NDB_No NVARCHAR(5) null,
       Add_Nutr_Mark NVARCHAR(1) null,
       Num_Studies INT null,
       Min FLOAT(53) null,
       Max FLOAT(53) null,
       DF INT null,
       Low_EB NVARCHAR(255) null,
       Up_EB FLOAT(53) null,
       Stat_cmt NVARCHAR(10) null,
       AddMod_Date NVARCHAR(10) null,
       CC NVARCHAR(1) null,
       dataDerivation NVARCHAR(4) null,
       dataSourceSet NVARCHAR(6) null,
       FoodDescription NVARCHAR(5) null,
       sourcecode NVARCHAR(2) null,
       primary key (NDB_No, Nutr_No)
    )

    create table NUTR_DEF (
        Nutr_No NVARCHAR(3) not null,
       Units NVARCHAR(7) not null,
       Tagname NVARCHAR(20) null,
       NutrDesc NVARCHAR(60) not null,
       Num_Dec NVARCHAR(1) not null,
       SR_Order INT not null,
       primary key (Nutr_No)
    )

    create table SRC_CD (
        Src_Cd NVARCHAR(2) not null,
       SrcCd_Desc NVARCHAR(60) not null,
       primary key (Src_Cd)
    )

    create table WEIGHT (
        NDB_No NVARCHAR(5) not null,
       Seq NVARCHAR(2) not null,
       Amount FLOAT(53) null,
       Msre_Desc NVARCHAR(84) null,
       Gm_Wgt FLOAT(53) null,
       Num_Data_Pts INT null,
       Std_Dev FLOAT(53) null,
       FoodDescription NVARCHAR(5) null,
       primary key (NDB_No, Seq)
    )

    alter table FOOD_DES
        add constraint FK_D12211F0
        foreign key (FdGrp_Cd)
        references FD_GROUP

    alter table FD_GROUP
        add constraint FK_84DD4256
        foreign key (foodGroup)
        references FD_GROUP

    alter table FOOTNOTE
        add constraint FK_9F99DBE6
        foreign key (NDB_No)
        references FOOD_DES

    alter table FOOTNOTE
        add constraint FK_A20340D5
        foreign key (Nutr_No)
        references NUTR_DEF

    alter table languageSet
        add constraint FK_EA522570
        foreign key (ITEM_ID)
        references FOOD_DES

    alter table languageSet
        add constraint FK_74F5CDE2
        foreign key (languageSet)
        references LANGDESC

    alter table NUT_DATA
        add constraint FK_302B1528
        foreign key (NDB_No)
        references FOOD_DES

    alter table NUT_DATA
        add constraint FK_C3AF5676
        foreign key (Nutr_No)
        references NUTR_DEF

    alter table NUT_DATA
        add constraint FK_B780E6BF
        foreign key (Src_Cd)
        references SRC_CD

    alter table NUT_DATA
        add constraint FK_93C3B1F7
        foreign key (Deriv_Cd)
        references DERIV_CD

    alter table NUT_DATA
        add constraint FK_6AAB7100
        foreign key (Ref_NDB_No)
        references FOOD_DES

    alter table NUT_DATA
        add constraint FK_1E10EDB6
        foreign key (dataDerivation)
        references DERIV_CD

    alter table NUT_DATA
        add constraint FK_3A4BB41B
        foreign key (dataSourceSet)
        references DATA_SRC

    alter table NUT_DATA
        add constraint FK_7ACFB724
        foreign key (FoodDescription)
        references FOOD_DES

    alter table NUT_DATA
        add constraint FK_E28454F1
        foreign key (sourcecode)
        references SRC_CD

    alter table WEIGHT
        add constraint FK_EF2818FD
        foreign key (NDB_No)
        references FOOD_DES

    alter table WEIGHT
        add constraint FK_2E80D0E2
        foreign key (FoodDescription)
        references FOOD_DES
Press any key to continue . . .