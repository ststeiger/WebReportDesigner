

DROP TABLE IF EXISTS Site;
CREATE TABLE Site(
	Site_UID uuid NULL,
	Site_No national character varying(10) NULL,
	Site_Text national character varying(200) NULL
	,CONSTRAINT PK_Site PRIMARY KEY (Site_UID) 
) ;






DROP TABLE IF EXISTS FloorType;
CREATE TABLE FloorType(
	FloorType_UID uuid NOT NULL,
	FloorType_Code national character varying(2) NULL,
	FloorType_Short_DE national character varying(50) NULL,
	FloorType_Short_FR national character varying(50) NULL,
	FloorType_Short_IT national character varying(50) NULL,
	FloorType_Short_EN national character varying(50) NULL,
	FloorType_Long_DE national character varying(255) NULL,
	FloorType_Long_FR national character varying(255) NULL,
	FloorType_Long_IT national character varying(255) NULL,
	FloorType_Long_EN national character varying(255) NULL,
	FloorType_Sort int NOT NULL,
	FloorType_MultiplicatorNo int NULL,
	FloorType_MEZ_Sort int NULL
	,CONSTRAINT PK_FloorType PRIMARY KEY (FloorType_UID) 
);



DROP TABLE IF EXISTS Floor;
CREATE TABLE Floor
(
  Floor_UID uuid,
  Floor_Building_UID uuid,
  Floor_FloorType_UID uuid,
  Floor_No integer,
  Floor_IsExterior boolean
  ,CONSTRAINT PK_Floor PRIMARY KEY (Floor_UID) 
);



DROP TABLE IF EXISTS Building;
CREATE TABLE Building 
(
  Building_UID uuid,
  Building_Site_UID uuid,
  Building_Nr character varying(10),
  Building_Text character varying(200),
  Building_No integer
  ,CONSTRAINT PK_Building PRIMARY KEY (Building_UID) 
);


-- ALTER TABLE Floor ALTER Floor_IsExterior TYPE bool USING CASE WHEN Floor_IsExterior=0 THEN FALSE ELSE TRUE END;
