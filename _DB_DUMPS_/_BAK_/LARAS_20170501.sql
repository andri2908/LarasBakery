-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: sys_pos_larasbakery
-- ------------------------------------------------------
-- Server version	5.5.28

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `account_credit`
--

DROP TABLE IF EXISTS `account_credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_credit` (
  `credit_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `credit_sales_id` int(10) unsigned NOT NULL,
  `credit_duedate` date NOT NULL,
  `credit_nominal` double NOT NULL,
  `credit_paid` tinyint(4) unsigned DEFAULT NULL,
  PRIMARY KEY (`credit_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_credit`
--

LOCK TABLES `account_credit` WRITE;
/*!40000 ALTER TABLE `account_credit` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_debt`
--

DROP TABLE IF EXISTS `account_debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_debt` (
  `debt_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `debt_purchase_id` int(10) unsigned NOT NULL,
  `debt_duedate` date NOT NULL,
  `debt_nominal` double NOT NULL,
  `debt_paid` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`debt_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_debt`
--

LOCK TABLES `account_debt` WRITE;
/*!40000 ALTER TABLE `account_debt` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `account_journal`
--

DROP TABLE IF EXISTS `account_journal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `account_journal` (
  `journal_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `account_id` smallint(5) unsigned NOT NULL,
  `journal_date` date NOT NULL,
  `account_nominal` double NOT NULL,
  `branch_id` tinyint(3) unsigned NOT NULL,
  `journal_description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`journal_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `account_journal`
--

LOCK TABLES `account_journal` WRITE;
/*!40000 ALTER TABLE `account_journal` DISABLE KEYS */;
/*!40000 ALTER TABLE `account_journal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cashier_log`
--

DROP TABLE IF EXISTS `cashier_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cashier_log` (
  `ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `USER_ID` tinyint(3) unsigned DEFAULT NULL,
  `DATE_LOGIN` datetime DEFAULT NULL,
  `DATE_LOGOUT` datetime DEFAULT NULL,
  `AMOUNT_START` double DEFAULT NULL,
  `AMOUNT_END` double DEFAULT NULL,
  `ACCOUNT_ID` smallint(5) unsigned DEFAULT NULL,
  `COMMENT` varchar(100) DEFAULT NULL,
  `TOTAL_CASH_TRANSACTION` double DEFAULT '0',
  `TOTAL_NON_CASH_TRANSACTION` double DEFAULT '0',
  `TOTAL_OTHER_TRANSACTION` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cashier_log`
--

LOCK TABLES `cashier_log` WRITE;
/*!40000 ALTER TABLE `cashier_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `cashier_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `close_shop_history`
--

DROP TABLE IF EXISTS `close_shop_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `close_shop_history` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `LAST_SUCCESS_LOCAL_DATE` datetime DEFAULT NULL,
  `LAST_SUCCESS_SERVER_DATE` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `close_shop_history`
--

LOCK TABLES `close_shop_history` WRITE;
/*!40000 ALTER TABLE `close_shop_history` DISABLE KEYS */;
INSERT INTO `close_shop_history` VALUES (2,'2017-05-01 00:00:00',NULL),(3,'2017-05-01 00:00:00',NULL),(4,'2017-05-01 00:00:00',NULL),(5,'2017-05-01 00:00:00',NULL),(6,'2017-05-01 00:00:00',NULL);
/*!40000 ALTER TABLE `close_shop_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credit`
--

DROP TABLE IF EXISTS `credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `credit` (
  `CREDIT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `CREDIT_DUE_DATE` date DEFAULT NULL,
  `CREDIT_NOMINAL` double DEFAULT '0',
  `CREDIT_PAID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`CREDIT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credit`
--

LOCK TABLES `credit` WRITE;
/*!40000 ALTER TABLE `credit` DISABLE KEYS */;
INSERT INTO `credit` VALUES (9,'SLO001-9',NULL,'2016-11-18',2000,0),(10,'SLO001-10',NULL,'2016-11-18',2000,0),(11,'SLO001-11',NULL,'2016-11-18',10000,0),(12,'SLO001-12',NULL,'2016-11-18',16000,0),(13,'SLO001-13',NULL,'2016-11-18',2000,0),(14,'SLO001-14',NULL,'2016-11-18',10000,0),(15,'SLO001-15',NULL,'2016-11-18',10000,0),(16,'SLO001-16',NULL,'2016-11-18',10000,0),(18,'SLO001-18',NULL,'2016-11-18',2000,0),(19,'SLO001-19',NULL,'2016-11-18',2000,0),(20,'SLO001-20',NULL,'2016-11-18',2000,0),(21,'SLO001-21',NULL,'2016-11-18',2000,0),(22,'SLO001-22',NULL,'2016-11-18',2000,0),(24,'SLO001-24',NULL,'2016-11-30',2000,0),(25,'SLO001-25',NULL,'2016-11-29',20000,0),(26,'SLO001-26',NULL,'2016-12-02',110000,0),(28,'SLO001-28',NULL,'2017-01-20',20000,0),(29,'SLO001-29',NULL,'2017-05-01',189000,0);
/*!40000 ALTER TABLE `credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `customer_product_disc`
--

DROP TABLE IF EXISTS `customer_product_disc`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customer_product_disc` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CUSTOMER_ID` smallint(6) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `DISC_1` double unsigned DEFAULT '0',
  `DISC_2` double unsigned DEFAULT '0',
  `DISC_RP` double unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `customer_product_disc`
--

LOCK TABLES `customer_product_disc` WRITE;
/*!40000 ALTER TABLE `customer_product_disc` DISABLE KEYS */;
INSERT INTO `customer_product_disc` VALUES (1,1,'RT002',0,0,0),(2,1,'RT001',0,0,0),(3,2,'4.1',0,0,0),(4,2,'RT004',0,0,0),(5,2,'RT003',0,0,0);
/*!40000 ALTER TABLE `customer_product_disc` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `daily_journal`
--

DROP TABLE IF EXISTS `daily_journal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_journal` (
  `journal_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `account_id` smallint(5) unsigned NOT NULL,
  `journal_datetime` datetime NOT NULL,
  `journal_nominal` double NOT NULL,
  `branch_id` tinyint(3) unsigned DEFAULT NULL,
  `journal_description` varchar(100) DEFAULT NULL,
  `user_id` tinyint(3) unsigned NOT NULL,
  `pm_id` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`journal_id`)
) ENGINE=InnoDB AUTO_INCREMENT=45 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `daily_journal`
--

LOCK TABLES `daily_journal` WRITE;
/*!40000 ALTER TABLE `daily_journal` DISABLE KEYS */;
INSERT INTO `daily_journal` VALUES (1,1,'2016-11-07 21:22:00',16000,NULL,'PEMBAYARAN SLO001-1',1,1),(2,1,'2016-11-14 22:50:00',4000,NULL,'PEMBAYARAN SLO001-2',1,1),(3,1,'2016-11-15 11:28:00',4000,NULL,'PEMBAYARAN SLO001-3',1,1),(4,1,'2016-11-15 11:30:00',4000,NULL,'PEMBAYARAN SLO001-4',1,1),(5,1,'2016-11-15 11:37:00',4000,NULL,'PEMBAYARAN SLO001-5',1,1),(6,1,'2016-11-15 11:50:00',4000,NULL,'PEMBAYARAN SLO001-6',1,1),(7,1,'2016-11-18 00:59:00',10000,NULL,'PEMBAYARAN SLO001-7',1,1),(8,1,'2016-11-18 11:38:00',2000,NULL,'PEMBAYARAN SLO001-8',1,1),(9,1,'2016-11-18 12:34:00',2000,NULL,'PEMBAYARAN SLO001-9',1,1),(10,1,'2016-11-18 12:42:00',2000,NULL,'PEMBAYARAN SLO001-10',1,1),(11,1,'2016-11-18 12:53:00',10000,NULL,'PEMBAYARAN SLO001-11',1,1),(12,1,'2016-11-18 13:06:00',16000,NULL,'PEMBAYARAN SLO001-12',1,1),(13,1,'2016-11-18 14:04:00',2000,NULL,'PEMBAYARAN SLO001-13',1,1),(14,1,'2016-11-18 14:06:00',10000,NULL,'PEMBAYARAN SLO001-14',1,1),(15,1,'2016-11-18 14:12:00',10000,NULL,'PEMBAYARAN SLO001-15',1,1),(16,1,'2016-11-18 14:22:00',10000,NULL,'PEMBAYARAN SLO001-16',1,1),(17,1,'2016-11-18 14:34:00',122,0,'PEMBAYARAN PIUTANG SLO001-9',1,1),(18,1,'2016-11-18 14:40:00',2000,NULL,'PEMBAYARAN SLO001-17',1,1),(19,1,'2016-11-18 14:55:00',2000,NULL,'PEMBAYARAN SLO001-18',1,1),(20,1,'2016-11-18 15:17:00',2000,NULL,'PEMBAYARAN SLO001-19',1,1),(21,1,'2016-11-18 15:22:00',2000,NULL,'PEMBAYARAN SLO001-20',1,1),(22,1,'2016-11-18 15:27:00',2000,NULL,'PEMBAYARAN SLO001-21',1,1),(23,1,'2016-11-18 15:33:00',2000,NULL,'PEMBAYARAN SLO001-22',1,1),(24,1,'2016-11-25 12:06:00',100,NULL,'PEMBAYARAN DP SQ-28',1,1),(25,1,'2016-11-25 12:07:00',900,NULL,'PEMBAYARAN DP SQ-28',1,1),(26,1,'2016-11-25 12:33:00',1000,NULL,'PEMBAYARAN DP SLO001-23',1,1),(27,1,'2016-11-28 12:11:00',1000,NULL,'PEMBAYARAN DP SQ-29',1,1),(28,1,'2016-11-30 11:43:00',2000,NULL,'PEMBAYARAN SLO001-24',1,1),(29,1,'2016-11-30 12:11:00',3000,NULL,'PEMBAYARAN DP SQ-30',1,1),(30,1,'2016-12-01 12:43:00',1000,NULL,'PEMBAYARAN DP SQ-31',1,1),(31,1,'2016-11-29 22:19:00',20000,NULL,'PEMBAYARAN SLO001-25',1,1),(32,1,'2016-12-02 22:26:00',50000,NULL,'PEMBAYARAN DP SQ-32',1,1),(33,1,'2016-12-02 22:26:00',-50000,NULL,'PEMBAYARAN DP SQ-32',1,1),(34,1,'2016-12-02 22:26:00',101000,NULL,'PEMBAYARAN SLO001-26',1,1),(35,1,'2016-12-02 22:46:00',2000,NULL,'PEMBAYARAN SLO001-27',1,1),(36,1,'2016-12-02 22:47:00',20000,NULL,'PEMBAYARAN DP SQ-34',1,1),(37,1,'2016-12-06 12:05:00',20000,NULL,'PEMBAYARAN DP SQ-35',1,1),(38,1,'2016-12-07 23:10:00',1000,NULL,'PEMBAYARAN DP SQ-36',1,1),(39,1,'2017-01-20 15:55:00',10000,NULL,'PEMBAYARAN DP SQ-37',1,1),(40,1,'2017-01-27 17:24:00',16000,NULL,'PEMBAYARAN SLO001-29',1,1),(41,1,'2017-01-27 23:12:00',16000,NULL,'PEMBAYARAN SLO001-30',1,1),(42,1,'2017-01-27 23:13:00',2000,NULL,'PEMBAYARAN SLO001-31',1,1),(43,1,'2017-05-01 12:17:00',189000,NULL,'PEMBAYARAN SLO001-29',1,1),(44,1,'2017-05-01 12:20:00',1000,NULL,'PEMBAYARAN DP SQ-38',1,1);
/*!40000 ALTER TABLE `daily_journal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `debt`
--

DROP TABLE IF EXISTS `debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `debt` (
  `DEBT_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `DEBT_DUE_DATE` date DEFAULT NULL,
  `DEBT_NOMINAL` double DEFAULT '0',
  `DEBT_PAID` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`DEBT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `debt`
--

LOCK TABLES `debt` WRITE;
/*!40000 ALTER TABLE `debt` DISABLE KEYS */;
INSERT INTO `debt` VALUES (1,'1','2016-11-07',4,0);
/*!40000 ALTER TABLE `debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_account`
--

DROP TABLE IF EXISTS `master_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_account` (
  `id` smallint(6) NOT NULL AUTO_INCREMENT,
  `account_id` int(10) unsigned NOT NULL,
  `account_name` varchar(50) NOT NULL,
  `account_type_id` tinyint(3) unsigned NOT NULL,
  `account_active` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `account_name_UNIQUE` (`account_name`),
  UNIQUE KEY `account_id_UNIQUE` (`account_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_account`
--

LOCK TABLES `master_account` WRITE;
/*!40000 ALTER TABLE `master_account` DISABLE KEYS */;
INSERT INTO `master_account` VALUES (1,1,'PENDAPATAN TUNAI PENJUALAN',1,1),(2,2,'PENDAPATAN BCA PENJUALAN',1,1),(3,3,'PIUTANG PENJUALAN',1,1),(4,4,'BEBAN GAJI PUSAT',2,1),(5,5,'BEBAN LISTRIK PUSAT',2,1),(6,6,'BEBAN AIR',2,1),(7,7,'PENDAPATAN LAIN-LAIN',1,1),(8,8,'BEBAN LAIN-LAIN',2,1),(9,10,'BEBAN',2,1);
/*!40000 ALTER TABLE `master_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_account_type`
--

DROP TABLE IF EXISTS `master_account_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_account_type` (
  `account_type_id` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `account_type_name` varchar(45) NOT NULL,
  PRIMARY KEY (`account_type_id`),
  UNIQUE KEY `account_type_name_UNIQUE` (`account_type_name`),
  UNIQUE KEY `account_type_id_UNIQUE` (`account_type_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_account_type`
--

LOCK TABLES `master_account_type` WRITE;
/*!40000 ALTER TABLE `master_account_type` DISABLE KEYS */;
INSERT INTO `master_account_type` VALUES (2,'CREDIT'),(1,'DEBET');
/*!40000 ALTER TABLE `master_account_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_branch`
--

DROP TABLE IF EXISTS `master_branch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_branch` (
  `BRANCH_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_NAME` varchar(50) NOT NULL,
  `BRANCH_ADDRESS_1` varchar(50) DEFAULT NULL,
  `BRANCH_ADDRESS_2` varchar(50) DEFAULT NULL,
  `BRANCH_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `BRANCH_TELEPHONE` varchar(15) DEFAULT NULL,
  `BRANCH_IP4` varchar(15) NOT NULL,
  `BRANCH_ACTIVE` tinyint(1) unsigned DEFAULT NULL,
  PRIMARY KEY (`BRANCH_ID`),
  UNIQUE KEY `BRANCH_NAME_UNIQUE` (`BRANCH_NAME`),
  UNIQUE KEY `BRANCH_IP4_UNIQUE` (`BRANCH_IP4`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_branch`
--

LOCK TABLES `master_branch` WRITE;
/*!40000 ALTER TABLE `master_branch` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_branch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_category`
--

DROP TABLE IF EXISTS `master_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_category` (
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `CATEGORY_NAME` varchar(50) DEFAULT NULL,
  `CATEGORY_DESCRIPTION` varchar(100) DEFAULT NULL,
  `CATEGORY_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`CATEGORY_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_category`
--

LOCK TABLES `master_category` WRITE;
/*!40000 ALTER TABLE `master_category` DISABLE KEYS */;
INSERT INTO `master_category` VALUES (1,'ROTI','ROTI ROTI',1);
/*!40000 ALTER TABLE `master_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_change_id`
--

DROP TABLE IF EXISTS `master_change_id`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_change_id` (
  `CHANGE_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CHANGE_ID_DESCRIPTION` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`CHANGE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_change_id`
--

LOCK TABLES `master_change_id` WRITE;
/*!40000 ALTER TABLE `master_change_id` DISABLE KEYS */;
INSERT INTO `master_change_id` VALUES (1,'LOGIN'),(2,'LOGOUT'),(3,'INSERT'),(4,'UPDATE'),(5,'SET NON ACTIVE'),(6,'CASHIER LOGIN'),(7,'CASHIER LOGOUT'),(8,'PAYMENT CREDIT'),(9,'PAYMENT DEBT');
/*!40000 ALTER TABLE `master_change_id` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_customer`
--

DROP TABLE IF EXISTS `master_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_customer` (
  `CUSTOMER_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `CUSTOMER_FULL_NAME` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS1` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS2` varchar(50) DEFAULT NULL,
  `CUSTOMER_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `CUSTOMER_PHONE` varchar(15) DEFAULT NULL,
  `CUSTOMER_FAX` varchar(15) DEFAULT NULL,
  `CUSTOMER_EMAIL` varchar(50) DEFAULT NULL,
  `CUSTOMER_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  `CUSTOMER_JOINED_DATE` date DEFAULT NULL,
  `CUSTOMER_TOTAL_SALES_COUNT` int(11) DEFAULT NULL,
  `CUSTOMER_GROUP` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`CUSTOMER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_customer`
--

LOCK TABLES `master_customer` WRITE;
/*!40000 ALTER TABLE `master_customer` DISABLE KEYS */;
INSERT INTO `master_customer` VALUES (1,'ANDRI','AAA','AAA','AAA','111',' ','111',1,'2016-11-15',0,1),(2,'ANTON',' ',' ',' ',' ',' ',' ',1,'2017-01-20',0,1);
/*!40000 ALTER TABLE `master_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_group`
--

DROP TABLE IF EXISTS `master_group`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_group` (
  `GROUP_ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `GROUP_USER_NAME` varchar(50) NOT NULL,
  `GROUP_USER_DESCRIPTION` varchar(100) NOT NULL,
  `GROUP_USER_ACTIVE` tinyint(1) unsigned NOT NULL,
  `GROUP_IS_CASHIER` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`GROUP_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_group`
--

LOCK TABLES `master_group` WRITE;
/*!40000 ALTER TABLE `master_group` DISABLE KEYS */;
INSERT INTO `master_group` VALUES (1,'GLOBAL_ADMIN','GLOBAL ADMIN GROUP',1,0),(2,'KASIR','AKSES HANYA MODUL PENJUALAN',1,1);
/*!40000 ALTER TABLE `master_group` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_message`
--

DROP TABLE IF EXISTS `master_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_message` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `STATUS` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(5) unsigned DEFAULT NULL,
  `IDENTIFIER_NO` varchar(45) DEFAULT NULL,
  `MSG_DATETIME_CREATED` datetime DEFAULT NULL,
  `MSG_CONTENT` varchar(200) DEFAULT NULL,
  `MSG_DATETIME_READ` datetime DEFAULT NULL,
  `MSG_READ_USER_ID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=55 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_message`
--

LOCK TABLES `master_message` WRITE;
/*!40000 ALTER TABLE `master_message` DISABLE KEYS */;
INSERT INTO `master_message` VALUES (1,0,31,'1','2016-11-11 00:00:00','PURCHASE ORDER [1] JATUH TEMPO TGL 07-November-2016',NULL,NULL),(2,0,37,'SLO001-9','2017-01-20 00:00:00','SALES INVOICE [SLO001-9] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(3,0,37,'SLO001-10','2017-01-20 00:00:00','SALES INVOICE [SLO001-10] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(4,0,37,'SLO001-11','2017-01-20 00:00:00','SALES INVOICE [SLO001-11] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(5,0,37,'SLO001-12','2017-01-20 00:00:00','SALES INVOICE [SLO001-12] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(6,0,37,'SLO001-13','2017-01-20 00:00:00','SALES INVOICE [SLO001-13] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(7,0,37,'SLO001-14','2017-01-20 00:00:00','SALES INVOICE [SLO001-14] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(8,0,37,'SLO001-15','2017-01-20 00:00:00','SALES INVOICE [SLO001-15] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(9,0,37,'SLO001-16','2017-01-20 00:00:00','SALES INVOICE [SLO001-16] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(10,0,37,'SLO001-18','2017-01-20 00:00:00','SALES INVOICE [SLO001-18] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(11,0,37,'SLO001-19','2017-01-20 00:00:00','SALES INVOICE [SLO001-19] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(12,0,37,'SLO001-20','2017-01-20 00:00:00','SALES INVOICE [SLO001-20] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(13,0,37,'SLO001-21','2017-01-20 00:00:00','SALES INVOICE [SLO001-21] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(14,0,37,'SLO001-22','2017-01-20 00:00:00','SALES INVOICE [SLO001-22] JATUH TEMPO TGL 18-November-2016',NULL,NULL),(15,0,37,'SLO001-24','2017-01-20 00:00:00','SALES INVOICE [SLO001-24] JATUH TEMPO TGL 30-November-2016',NULL,NULL),(16,0,37,'SLO001-25','2017-01-20 00:00:00','SALES INVOICE [SLO001-25] JATUH TEMPO TGL 29-November-2016',NULL,NULL),(17,0,37,'SLO001-26','2017-01-20 00:00:00','SALES INVOICE [SLO001-26] JATUH TEMPO TGL 02-December-2016',NULL,NULL),(18,0,37,'SLO001-28','2017-01-20 00:00:00','SALES INVOICE [SLO001-28] JATUH TEMPO TGL 20-January-2017',NULL,NULL),(19,0,56,'SQ-2','2017-01-20 00:00:00','QUOTATION [SQ-2] DIPESAN TGL 17-November-2016',NULL,NULL),(20,0,56,'SQ-3','2017-01-20 00:00:00','QUOTATION [SQ-3] DIPESAN TGL 16-November-2016',NULL,NULL),(21,0,56,'SQ-4','2017-01-20 00:00:00','QUOTATION [SQ-4] DIPESAN TGL 16-November-2016',NULL,NULL),(22,0,56,'SQ-5','2017-01-20 00:00:00','QUOTATION [SQ-5] DIPESAN TGL 16-November-2016',NULL,NULL),(23,0,56,'SQ-6','2017-01-20 00:00:00','QUOTATION [SQ-6] DIPESAN TGL 16-November-2016',NULL,NULL),(24,0,56,'SQ-7','2017-01-20 00:00:00','QUOTATION [SQ-7] DIPESAN TGL 16-November-2016',NULL,NULL),(25,0,56,'SQ-8','2017-01-20 00:00:00','QUOTATION [SQ-8] DIPESAN TGL 16-November-2016',NULL,NULL),(26,0,56,'SQ-9','2017-01-20 00:00:00','QUOTATION [SQ-9] DIPESAN TGL 16-November-2016',NULL,NULL),(27,0,56,'SQ-10','2017-01-20 00:00:00','QUOTATION [SQ-10] DIPESAN TGL 16-November-2016',NULL,NULL),(28,0,56,'SQ-11','2017-01-20 00:00:00','QUOTATION [SQ-11] DIPESAN TGL 16-November-2016',NULL,NULL),(29,0,56,'SQ-12','2017-01-20 00:00:00','QUOTATION [SQ-12] DIPESAN TGL 16-November-2016',NULL,NULL),(30,0,56,'SQ-13','2017-01-20 00:00:00','QUOTATION [SQ-13] DIPESAN TGL 19-November-2016',NULL,NULL),(31,0,56,'SQ-14','2017-01-20 00:00:00','QUOTATION [SQ-14] DIPESAN TGL 18-November-2016',NULL,NULL),(32,0,56,'SQ-15','2017-01-20 00:00:00','QUOTATION [SQ-15] DIPESAN TGL 19-November-2016',NULL,NULL),(33,0,56,'SQ-16','2017-01-20 00:00:00','QUOTATION [SQ-16] DIPESAN TGL 24-November-2016',NULL,NULL),(34,0,56,'SQ-17','2017-01-20 00:00:00','QUOTATION [SQ-17] DIPESAN TGL 18-November-2016',NULL,NULL),(35,0,56,'SQ-18','2017-01-20 00:00:00','QUOTATION [SQ-18] DIPESAN TGL 18-November-2016',NULL,NULL),(36,0,56,'SQ-19','2017-01-20 00:00:00','QUOTATION [SQ-19] DIPESAN TGL 18-November-2016',NULL,NULL),(37,0,56,'SQ-20','2017-01-20 00:00:00','QUOTATION [SQ-20] DIPESAN TGL 18-November-2016',NULL,NULL),(38,0,56,'SQ-21','2017-01-20 00:00:00','QUOTATION [SQ-21] DIPESAN TGL 18-November-2016',NULL,NULL),(39,0,56,'SQ-22','2017-01-20 00:00:00','QUOTATION [SQ-22] DIPESAN TGL 18-November-2016',NULL,NULL),(40,0,56,'SQ-23','2017-01-20 00:00:00','QUOTATION [SQ-23] DIPESAN TGL 18-November-2016',NULL,NULL),(41,0,56,'SQ-24','2017-01-20 00:00:00','QUOTATION [SQ-24] DIPESAN TGL 18-November-2016',NULL,NULL),(42,0,56,'SQ-25','2017-01-20 00:00:00','QUOTATION [SQ-25] DIPESAN TGL 18-November-2016',NULL,NULL),(43,0,56,'SQ-26','2017-01-20 00:00:00','QUOTATION [SQ-26] DIPESAN TGL 18-November-2016',NULL,NULL),(44,0,56,'SQ-27','2017-01-20 00:00:00','QUOTATION [SQ-27] DIPESAN TGL 18-November-2016',NULL,NULL),(45,0,56,'SQ-28','2017-01-20 00:00:00','QUOTATION [SQ-28] DIPESAN TGL 30-November-2016',NULL,NULL),(46,0,56,'SQ-29','2017-01-20 00:00:00','QUOTATION [SQ-29] DIPESAN TGL 30-November-2016',NULL,NULL),(47,0,56,'SQ-30','2017-01-20 00:00:00','QUOTATION [SQ-30] DIPESAN TGL 01-December-2016',NULL,NULL),(48,0,56,'SQ-31','2017-01-20 00:00:00','QUOTATION [SQ-31] DIPESAN TGL 02-December-2016',NULL,NULL),(49,0,56,'SQ-32','2017-01-20 00:00:00','QUOTATION [SQ-32] DIPESAN TGL 05-December-2016',NULL,NULL),(50,0,56,'SQ-33','2017-01-20 00:00:00','QUOTATION [SQ-33] DIPESAN TGL 02-December-2016',NULL,NULL),(51,0,56,'SQ-34','2017-01-20 00:00:00','QUOTATION [SQ-34] DIPESAN TGL 02-December-2016',NULL,NULL),(52,0,56,'SQ-35','2017-01-20 00:00:00','QUOTATION [SQ-35] DIPESAN TGL 06-December-2016',NULL,NULL),(53,0,56,'SQ-36','2017-01-20 00:00:00','QUOTATION [SQ-36] DIPESAN TGL 07-December-2016',NULL,NULL),(54,0,56,'SQ-37','2017-01-20 00:00:00','QUOTATION [SQ-37] DIPESAN TGL 26-January-2017',NULL,NULL);
/*!40000 ALTER TABLE `master_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_module`
--

DROP TABLE IF EXISTS `master_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_module` (
  `MODULE_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `MODULE_NAME` varchar(50) DEFAULT NULL,
  `MODULE_DESCRIPTION` varchar(100) DEFAULT NULL,
  `MODULE_FEATURES` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`MODULE_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_module`
--

LOCK TABLES `master_module` WRITE;
/*!40000 ALTER TABLE `master_module` DISABLE KEYS */;
INSERT INTO `master_module` VALUES (1,'MANAJEMEN SISTEM',NULL,1,1),(2,'DATABASE',NULL,1,1),(3,'MANAJEMEN USER',NULL,3,1),(4,'MANAJEMEN CABANG',NULL,3,1),(5,'SINKRONISASI INFORMASI',NULL,1,1),(6,'PENGATURAN PRINTER',NULL,1,1),(7,'PENGATURAN GAMBAR LATAR',NULL,1,1),(8,'GUDANG',NULL,1,1),(9,'PRODUK',NULL,1,1),(10,'TAMBAH / UPDATE PRODUK',NULL,3,1),(11,'PENGATURAN HARGA PRODUK',NULL,1,1),(12,'PENGATURAN LIMIT STOK',NULL,1,1),(13,'PENGATURAN KATEGORI PRODUK',NULL,1,1),(14,'PECAH SATUAN PRODUK',NULL,1,1),(15,'PENGATURAN NOMOR RAK',NULL,1,1),(16,'KATEGORI PRODUK',NULL,3,1),(17,'SATUAN PRODUK',NULL,1,1),(18,'TAMBAH / UPDATE SATUAN',NULL,1,1),(19,'PENGATURAN KONVERSI SATUAN',NULL,1,1),(20,'STOK OPNAME',NULL,1,1),(21,'PENYESUAIAN STOK',NULL,1,1),(22,'MUTASI BARANG',NULL,1,1),(23,'TAMBAH / UPDATE MUTASI BARANG',NULL,3,1),(24,'CEK PERMINTAAN BARANG',NULL,1,1),(25,'PENERIMAAN BARANG',NULL,1,1),(26,'PENERIMAAN BARANG DARI MUTASI',NULL,1,1),(27,'PENERIMAAN BARANG DARI PO',NULL,1,1),(28,'PEMBELIAN',NULL,1,1),(29,'SUPPLIER',NULL,1,1),(30,'REQUEST ORDER',NULL,3,1),(31,'PURCHASE ORDER',NULL,3,1),(32,'REPRINT REQUEST ORDER',NULL,1,1),(33,'RETUR PEMBELIAN KE SUPPLIER',NULL,1,1),(34,'RETUR PERMINTAAN KE PUSAT',NULL,1,1),(35,'PENJUALAN',NULL,1,1),(36,'PELANGGAN',NULL,3,1),(37,'TRANSAKSI PENJUALAN',NULL,1,1),(38,'SET NO FAKTUR',NULL,1,1),(39,'RETUR PENJUALAN',NULL,1,1),(40,'RETUR PENJUALAN BY INVOICE',NULL,1,1),(41,'RETUR PENJUALAN BY STOK',NULL,1,1),(42,'KEUANGAN',NULL,1,1),(43,'PENGATURAN NO AKUN',NULL,3,1),(44,'TRANSAKSI',NULL,1,1),(45,'TAMBAH TRANSAKSI HARIAN',NULL,1,1),(46,'PEMBAYARAN PIUTANG',NULL,1,1),(47,'PEMBAYARAN PIUTANG MUTASI',NULL,1,1),(48,'PEMBAYARAN HUTANG KE SUPPLIER',NULL,1,1),(49,'PENGATURAN LIMIT PAJAK',NULL,1,1),(50,'MODUL MESSAGING',NULL,1,1),(51,'TAX_MODULE',NULL,1,1),(52,'SALES QUOTATION',NULL,1,1),(53,'APPROVAL SALES QUOTATION',NULL,1,1),(54,'PENGATURAN HARGA POKOK',NULL,1,1),(55,'REVISI STOCK TAKE HARIAN',NULL,1,1),(56,'SALES QUOTATION',NULL,1,1);
/*!40000 ALTER TABLE `master_module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_product`
--

DROP TABLE IF EXISTS `master_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_product` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(50) DEFAULT '',
  `PRODUCT_BARCODE` varchar(15) DEFAULT '0',
  `PRODUCT_NAME` varchar(50) DEFAULT '',
  `PRODUCT_DESCRIPTION` varchar(100) DEFAULT '',
  `PRODUCT_BASE_PRICE` double DEFAULT '0',
  `PRODUCT_RETAIL_PRICE` double DEFAULT '0',
  `PRODUCT_BULK_PRICE` double DEFAULT '0',
  `PRODUCT_WHOLESALE_PRICE` double DEFAULT '0',
  `PRODUCT_PHOTO_1` varchar(50) DEFAULT '',
  `UNIT_ID` smallint(5) unsigned DEFAULT '0',
  `PRODUCT_BS_PRICE` double DEFAULT '0',
  `PRODUCT_STOCK_QTY` double DEFAULT '0',
  `PRODUCT_STOCK_AWAL` double DEFAULT '0',
  `PRODUCT_BS_QTY` double DEFAULT '0',
  `PRODUCT_LIMIT_STOCK` double DEFAULT '0',
  `PRODUCT_SHELVES` varchar(5) DEFAULT '--00',
  `PRODUCT_ACTIVE` tinyint(3) unsigned DEFAULT '0',
  `PRODUCT_BRAND` varchar(50) DEFAULT '',
  `PRODUCT_IS_SERVICE` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PRODUCT_ID_UNIQUE` (`PRODUCT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_product`
--

LOCK TABLES `master_product` WRITE;
/*!40000 ALTER TABLE `master_product` DISABLE KEYS */;
INSERT INTO `master_product` VALUES (1,'RT001','RT001','ROTI PISANG',' ',2,1000,0,0,' ',1,800,60,60,0,0,'--00',1,' ',0),(2,'RT002','RT002','ROTI PISANG COKLAT',' ',0,5000,0,0,' ',1,4500,74,74,0,0,'--00',1,' ',0),(3,'RT003','RT003','ROTI PISANG COKLAT KEJU',' ',0,8000,0,0,' ',1,5000,74,74,0,0,'--00',1,' ',0),(4,'4.1','0001','ROTI KELAPA','ROTI KELAPA',1000,2500,0,0,' ',1,0,115,115,0,0,'--00',1,' ',0),(5,'RT004','0','KENTANG GORENG','KENTANG',100,1000,0,0,' ',1,10,987,987,0,0,'--00',1,' ',0);
/*!40000 ALTER TABLE `master_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_supplier`
--

DROP TABLE IF EXISTS `master_supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_supplier` (
  `SUPPLIER_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `SUPPLIER_FULL_NAME` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS1` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS2` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `SUPPLIER_PHONE` varchar(15) DEFAULT NULL,
  `SUPPLIER_FAX` varchar(15) DEFAULT NULL,
  `SUPPLIER_EMAIL` varchar(50) DEFAULT NULL,
  `SUPPLIER_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`SUPPLIER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_supplier`
--

LOCK TABLES `master_supplier` WRITE;
/*!40000 ALTER TABLE `master_supplier` DISABLE KEYS */;
INSERT INTO `master_supplier` VALUES (1,'PABRIK','AA','AA','AA','11',' ','11',1);
/*!40000 ALTER TABLE `master_supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_unit`
--

DROP TABLE IF EXISTS `master_unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_unit` (
  `UNIT_ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `UNIT_NAME` varchar(50) DEFAULT NULL,
  `UNIT_DESCRIPTION` varchar(100) DEFAULT NULL,
  `UNIT_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`UNIT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_unit`
--

LOCK TABLES `master_unit` WRITE;
/*!40000 ALTER TABLE `master_unit` DISABLE KEYS */;
INSERT INTO `master_unit` VALUES (1,'BIJI','BIJI',1);
/*!40000 ALTER TABLE `master_unit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_user`
--

DROP TABLE IF EXISTS `master_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_user` (
  `ID` tinyint(3) unsigned NOT NULL AUTO_INCREMENT,
  `USER_NAME` varchar(15) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `USER_PASSWORD` varchar(15) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  `USER_FULL_NAME` varchar(50) DEFAULT NULL,
  `USER_PHONE` varchar(15) DEFAULT NULL,
  `USER_ACTIVE` tinyint(1) unsigned DEFAULT NULL,
  `GROUP_ID` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `USER_NAME_UNIQUE` (`USER_NAME`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='	';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_user`
--

LOCK TABLES `master_user` WRITE;
/*!40000 ALTER TABLE `master_user` DISABLE KEYS */;
INSERT INTO `master_user` VALUES (1,'ADMIN','admin','ADMIN','1',1,1);
/*!40000 ALTER TABLE `master_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_credit`
--

DROP TABLE IF EXISTS `payment_credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_credit` (
  `payment_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `credit_id` int(11) NOT NULL,
  `payment_date` date NOT NULL,
  `pm_id` tinyint(3) NOT NULL,
  `payment_nominal` double NOT NULL,
  `payment_description` varchar(100) DEFAULT NULL,
  `payment_confirmed` tinyint(3) DEFAULT NULL,
  `payment_invalid` tinyint(4) DEFAULT '0',
  `payment_confirmed_date` date DEFAULT NULL,
  `payment_due_date` date DEFAULT NULL,
  `payment_is_dp` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_credit`
--

LOCK TABLES `payment_credit` WRITE;
/*!40000 ALTER TABLE `payment_credit` DISABLE KEYS */;
INSERT INTO `payment_credit` VALUES (1,10,'2016-11-18',1,1000,'DP',1,0,'2016-11-18',NULL,0),(2,11,'2016-11-18',1,2000,'DP',1,0,'2016-11-18',NULL,0),(3,12,'2016-11-18',1,1000,'DP',1,0,'2016-11-18',NULL,0),(4,13,'2016-11-18',1,1000,'DP',1,0,'2016-11-18',NULL,0),(5,15,'2016-11-18',1,1000,'DP',1,0,'2016-11-18',NULL,0),(6,16,'2016-11-18',1,6078,'DP',1,0,'2016-11-18',NULL,0),(7,9,'2016-11-18',1,122,'',1,0,'2016-11-18','2016-11-18',0),(8,18,'2016-11-18',1,1000,'DP',1,0,'2016-11-18','2016-11-18',0),(9,21,'2016-11-18',1,100,'DP',1,0,'2016-11-18','2016-11-18',0),(10,22,'2016-11-18',1,100,'DP',1,0,'2016-11-15','2016-11-15',0),(11,24,'2016-11-30',1,1000,'DP',1,0,'2016-11-25','2016-11-25',0),(12,28,'2017-01-20',1,10000,'DP',1,0,'2017-01-20','2017-01-20',1);
/*!40000 ALTER TABLE `payment_credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_debt`
--

DROP TABLE IF EXISTS `payment_debt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_debt` (
  `payment_id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `debt_id` int(11) NOT NULL,
  `payment_date` date NOT NULL,
  `pm_id` tinyint(3) NOT NULL,
  `payment_nominal` double NOT NULL,
  `payment_description` varchar(100) DEFAULT NULL,
  `payment_confirmed` tinyint(3) DEFAULT '0',
  `payment_invalid` tinyint(4) DEFAULT '0',
  `payment_confirmed_date` date DEFAULT NULL,
  `payment_due_date` date DEFAULT NULL,
  PRIMARY KEY (`payment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_debt`
--

LOCK TABLES `payment_debt` WRITE;
/*!40000 ALTER TABLE `payment_debt` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_debt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment_method`
--

DROP TABLE IF EXISTS `payment_method`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment_method` (
  `pm_id` tinyint(4) NOT NULL AUTO_INCREMENT,
  `pm_name` varchar(15) NOT NULL,
  `pm_description` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`pm_id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_method`
--

LOCK TABLES `payment_method` WRITE;
/*!40000 ALTER TABLE `payment_method` DISABLE KEYS */;
INSERT INTO `payment_method` VALUES (1,'TUNAI','TUNAI'),(2,'KARTU KREDIT','KARTU KREDIT'),(3,'KARTU DEBIT','KARTU DEBIT'),(4,'TRANSFER','TRANSFER BANK'),(5,'BG','BILYET GIRO'),(6,'CEK','CEK');
/*!40000 ALTER TABLE `payment_method` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_adjustment`
--

DROP TABLE IF EXISTS `product_adjustment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_adjustment` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(30) DEFAULT NULL,
  `PRODUCT_ADJUSTMENT_DATE` date DEFAULT NULL,
  `PRODUCT_OLD_STOCK_QTY` double DEFAULT NULL,
  `PRODUCT_NEW_STOCK_QTY` double DEFAULT NULL,
  `PRODUCT_ADJUSTMENT_DESCRIPTION` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_adjustment`
--

LOCK TABLES `product_adjustment` WRITE;
/*!40000 ALTER TABLE `product_adjustment` DISABLE KEYS */;
INSERT INTO `product_adjustment` VALUES (1,'RT002','2016-11-09',10,12,' '),(2,'RT003','2016-11-09',8,5,' '),(3,'RT003','2016-11-16',0,100,' '),(4,'RT002','2016-11-18',0,100,' '),(5,'RT001','2016-11-18',0,100,' ');
/*!40000 ALTER TABLE `product_adjustment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_category`
--

DROP TABLE IF EXISTS `product_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_category` (
  `PRODUCT_ID` varchar(50) NOT NULL,
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`PRODUCT_ID`,`CATEGORY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_category`
--

LOCK TABLES `product_category` WRITE;
/*!40000 ALTER TABLE `product_category` DISABLE KEYS */;
INSERT INTO `product_category` VALUES ('4.1',1),('RT001',1),('RT002',1),('RT003',1),('RT004',1);
/*!40000 ALTER TABLE `product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_daily_adjustment_detail`
--

DROP TABLE IF EXISTS `product_daily_adjustment_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_daily_adjustment_detail` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRODUCT_ADJUSTMENT_ID` int(11) DEFAULT NULL,
  `PRODUCT_ID` varchar(45) DEFAULT NULL,
  `PRODUCT_LAST_STOCK_QTY` double DEFAULT '0',
  `PRODUCT_RECEIVED_QTY` double DEFAULT '0',
  `PRODUCT_BS_QTY` double DEFAULT '0',
  `PRODUCT_SOLD_QTY` double DEFAULT '0',
  `PRODUCT_LEFTOVER_QTY` double DEFAULT '0',
  `REMARKS` varchar(100) DEFAULT NULL,
  `PRODUCT_RIIL_QTY` double DEFAULT NULL,
  `PRODUCT_ADJUSTMENT_QTY` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_daily_adjustment_detail`
--

LOCK TABLES `product_daily_adjustment_detail` WRITE;
/*!40000 ALTER TABLE `product_daily_adjustment_detail` DISABLE KEYS */;
INSERT INTO `product_daily_adjustment_detail` VALUES (1,1,'RT001',10,2,3,0,9,'aaaa',NULL,NULL),(2,1,'RT002',10,0,3,0,7,'sss',NULL,NULL),(3,1,'RT003',8,0,1,2,5,'ddddd',NULL,NULL),(10,2,'RT001',12,0,3,0,9,'aa',0,0),(11,2,'RT002',10,0,4,0,6,'ss',2,0),(12,2,'RT003',8,0,0,0,8,'dd',-3,0),(13,3,'RT001',0,0,0,0,12,'aa',12,12),(14,3,'RT002',0,0,0,0,12,'aa',12,12),(15,3,'RT003',0,0,0,0,5,'aa',5,5),(19,4,'RT001',12,0,2,0,10,'aaaaa',0,0),(20,4,'RT002',12,0,0,0,12,'dsf',0,0),(21,4,'RT003',5,0,0,0,5,'sdf',0,0),(47,5,'RT001',0,0,0,0,60,'',60,60),(48,5,'RT002',0,0,0,0,74,'',74,74),(49,5,'RT003',0,0,0,22,74,'',74,96),(50,5,'4.1',123,0,0,0,115,'',115,-8),(51,5,'RT004',1000,0,0,13,987,'',987,0);
/*!40000 ALTER TABLE `product_daily_adjustment_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_daily_adjustment_header`
--

DROP TABLE IF EXISTS `product_daily_adjustment_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_daily_adjustment_header` (
  `PRODUCT_ADJUSTMENT_ID` int(11) NOT NULL,
  `PRODUCT_ADJUSTMENT_DATE` date DEFAULT NULL,
  `USER_ID` tinyint(4) DEFAULT NULL,
  `STOCK_TAKE_CLOSED` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`PRODUCT_ADJUSTMENT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_daily_adjustment_header`
--

LOCK TABLES `product_daily_adjustment_header` WRITE;
/*!40000 ALTER TABLE `product_daily_adjustment_header` DISABLE KEYS */;
INSERT INTO `product_daily_adjustment_header` VALUES (1,'2016-11-07',1,0),(2,'2016-11-10',1,0),(3,'2016-11-11',1,1),(4,'2017-04-26',1,1),(5,'2017-05-01',1,1);
/*!40000 ALTER TABLE `product_daily_adjustment_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_daily_adjustment_history`
--

DROP TABLE IF EXISTS `product_daily_adjustment_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_daily_adjustment_history` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PRODUCT_ADJUSTMENT_ID` int(11) DEFAULT NULL,
  `PRODUCT_REVISION_NO` int(11) DEFAULT NULL,
  `REVISION_REMARK` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_daily_adjustment_history`
--

LOCK TABLES `product_daily_adjustment_history` WRITE;
/*!40000 ALTER TABLE `product_daily_adjustment_history` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_daily_adjustment_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product_loss`
--

DROP TABLE IF EXISTS `product_loss`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product_loss` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PL_DATETIME` date DEFAULT NULL,
  `PRODUCT_ID` int(10) unsigned DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `NEW_PRODUCT_ID` int(10) unsigned DEFAULT NULL,
  `NEW_PRODUCT_QTY` double DEFAULT NULL,
  `TOTAL_LOSS` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product_loss`
--

LOCK TABLES `product_loss` WRITE;
/*!40000 ALTER TABLE `product_loss` DISABLE KEYS */;
/*!40000 ALTER TABLE `product_loss` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_mutation_detail`
--

DROP TABLE IF EXISTS `products_mutation_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_mutation_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PM_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_mutation_detail`
--

LOCK TABLES `products_mutation_detail` WRITE;
/*!40000 ALTER TABLE `products_mutation_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `products_mutation_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_mutation_header`
--

DROP TABLE IF EXISTS `products_mutation_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_mutation_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `BRANCH_ID_FROM` tinyint(3) unsigned DEFAULT NULL,
  `BRANCH_ID_TO` tinyint(3) unsigned DEFAULT NULL,
  `PM_DATETIME` date DEFAULT NULL,
  `PM_TOTAL` double DEFAULT NULL,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `PM_RECEIVED` tinyint(4) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_mutation_header`
--

LOCK TABLES `products_mutation_header` WRITE;
/*!40000 ALTER TABLE `products_mutation_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `products_mutation_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_received_detail`
--

DROP TABLE IF EXISTS `products_received_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_received_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PR_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PRODUCT_ACTUAL_QTY` double DEFAULT NULL,
  `PR_SUBTOTAL` double DEFAULT NULL,
  `PRODUCT_PRICE_CHANGE` tinyint(4) DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_detail`
--

LOCK TABLES `products_received_detail` WRITE;
/*!40000 ALTER TABLE `products_received_detail` DISABLE KEYS */;
INSERT INTO `products_received_detail` VALUES (1,'1','RT001',2,2,2,4,0);
/*!40000 ALTER TABLE `products_received_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products_received_header`
--

DROP TABLE IF EXISTS `products_received_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products_received_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PR_INVOICE` varchar(30) DEFAULT NULL,
  `PR_FROM` tinyint(3) unsigned DEFAULT NULL,
  `PR_TO` tinyint(3) unsigned DEFAULT NULL,
  `PR_DATE` date DEFAULT NULL,
  `PR_TOTAL` double DEFAULT NULL,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PR_INVOICE_UNIQUE` (`PR_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='		';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products_received_header`
--

LOCK TABLES `products_received_header` WRITE;
/*!40000 ALTER TABLE `products_received_header` DISABLE KEYS */;
INSERT INTO `products_received_header` VALUES (1,'1',1,0,'2016-11-07',4,NULL,'1');
/*!40000 ALTER TABLE `products_received_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_detail`
--

DROP TABLE IF EXISTS `purchase_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PURCHASE_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_detail`
--

LOCK TABLES `purchase_detail` WRITE;
/*!40000 ALTER TABLE `purchase_detail` DISABLE KEYS */;
INSERT INTO `purchase_detail` VALUES (1,'1','RT001',2,2,4);
/*!40000 ALTER TABLE `purchase_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_detail_tax`
--

DROP TABLE IF EXISTS `purchase_detail_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_detail_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `PURCHASE_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_detail_tax`
--

LOCK TABLES `purchase_detail_tax` WRITE;
/*!40000 ALTER TABLE `purchase_detail_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase_detail_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_header`
--

DROP TABLE IF EXISTS `purchase_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `PURCHASE_DATETIME` date DEFAULT NULL,
  `PURCHASE_TOTAL` double DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT` tinyint(3) unsigned DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DURATION` double unsigned DEFAULT '0',
  `PURCHASE_DATE_RECEIVED` date DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DATE` date DEFAULT NULL,
  `PURCHASE_PAID` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_SENT` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_RECEIVED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PURCHASE_INVOICE_UNIQUE` (`PURCHASE_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_header`
--

LOCK TABLES `purchase_header` WRITE;
/*!40000 ALTER TABLE `purchase_header` DISABLE KEYS */;
INSERT INTO `purchase_header` VALUES (1,'1',1,'2016-11-07',4,1,0,'2016-11-07','2016-11-07',0,1,1);
/*!40000 ALTER TABLE `purchase_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchase_header_tax`
--

DROP TABLE IF EXISTS `purchase_header_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchase_header_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PURCHASE_INVOICE` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `PURCHASE_DATETIME` date DEFAULT NULL,
  `PURCHASE_TOTAL` double DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT` tinyint(3) unsigned DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DURATION` double unsigned DEFAULT '0',
  `PURCHASE_DATE_RECEIVED` date DEFAULT NULL,
  `PURCHASE_TERM_OF_PAYMENT_DATE` date DEFAULT NULL,
  `PURCHASE_PAID` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_SENT` tinyint(3) unsigned DEFAULT '0',
  `PURCHASE_RECEIVED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PURCHASE_INVOICE_UNIQUE` (`PURCHASE_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchase_header_tax`
--

LOCK TABLES `purchase_header_tax` WRITE;
/*!40000 ALTER TABLE `purchase_header_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `purchase_header_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_order_detail`
--

DROP TABLE IF EXISTS `request_order_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_order_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `RO_QTY` double DEFAULT NULL,
  `RO_SUBTOTAL` double DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_order_detail`
--

LOCK TABLES `request_order_detail` WRITE;
/*!40000 ALTER TABLE `request_order_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_order_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_order_header`
--

DROP TABLE IF EXISTS `request_order_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `request_order_header` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RO_INVOICE` varchar(30) DEFAULT NULL,
  `RO_BRANCH_ID_FROM` tinyint(3) unsigned DEFAULT NULL,
  `RO_BRANCH_ID_TO` tinyint(3) unsigned DEFAULT NULL,
  `RO_DATETIME` date DEFAULT NULL,
  `RO_TOTAL` double DEFAULT NULL,
  `RO_EXPIRED` date DEFAULT NULL,
  `RO_ACTIVE` tinyint(4) DEFAULT NULL,
  `RO_EXPORTED` tinyint(4) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `RO_INVOICE_UNIQUE` (`RO_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_order_header`
--

LOCK TABLES `request_order_header` WRITE;
/*!40000 ALTER TABLE `request_order_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_order_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_purchase_detail`
--

DROP TABLE IF EXISTS `return_purchase_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_purchase_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RP_ID` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BASEPRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `RP_DESCRIPTION` varchar(100) DEFAULT NULL,
  `RP_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_detail`
--

LOCK TABLES `return_purchase_detail` WRITE;
/*!40000 ALTER TABLE `return_purchase_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `return_purchase_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_purchase_header`
--

DROP TABLE IF EXISTS `return_purchase_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_purchase_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RP_ID` varchar(30) DEFAULT NULL,
  `SUPPLIER_ID` smallint(5) unsigned DEFAULT NULL,
  `RP_DATE` date DEFAULT NULL,
  `RP_TOTAL` double DEFAULT '0',
  `RP_PROCESSED` tinyint(3) unsigned DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `RP_INVOICE_UNIQUE` (`RP_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_purchase_header`
--

LOCK TABLES `return_purchase_header` WRITE;
/*!40000 ALTER TABLE `return_purchase_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `return_purchase_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_sales_detail`
--

DROP TABLE IF EXISTS `return_sales_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_sales_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RS_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_QTY` double DEFAULT '0',
  `PRODUCT_RETURN_QTY` double DEFAULT '0',
  `RS_DESCRIPTION` varchar(100) DEFAULT NULL,
  `RS_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_detail`
--

LOCK TABLES `return_sales_detail` WRITE;
/*!40000 ALTER TABLE `return_sales_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `return_sales_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `return_sales_header`
--

DROP TABLE IF EXISTS `return_sales_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `return_sales_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `RS_INVOICE` varchar(30) DEFAULT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT NULL,
  `RS_DATETIME` date DEFAULT NULL,
  `RS_TOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `return_sales_header`
--

LOCK TABLES `return_sales_header` WRITE;
/*!40000 ALTER TABLE `return_sales_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `return_sales_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail`
--

DROP TABLE IF EXISTS `sales_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `PRODUCT_DISC1` double DEFAULT '0',
  `PRODUCT_DISC2` double DEFAULT '0',
  `PRODUCT_DISC_RP` double DEFAULT '0',
  `SALES_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail`
--

LOCK TABLES `sales_detail` WRITE;
/*!40000 ALTER TABLE `sales_detail` DISABLE KEYS */;
INSERT INTO `sales_detail` VALUES (2,'SLO001-15','RT002',0,5000,2,0,0,0,10000),(3,'SLO001-16','RT002',0,5000,2,0,0,0,10000),(5,'SLO001-18','RT001',0,1000,2,0,0,0,2000),(6,'SLO001-19','RT001',0,1000,2,0,0,0,2000),(7,'SLO001-20','RT001',0,1000,2,0,0,0,2000),(8,'SLO001-21','RT001',0,1000,2,0,0,0,2000),(9,'SLO001-22','RT001',0,1000,2,0,0,0,2000),(11,'SLO001-24','RT001',0,1000,2,0,0,0,2000),(12,'SLO001-25','RT001',2,1000,20,0,0,0,20000),(13,'SLO001-26','RT002',0,5000,22,0,0,0,110000),(15,'SLO001-28','4.1',0,2500,8,0,0,0,20000),(16,'SLO001-29','RT004',100,1000,13,0,0,0,13000),(17,'SLO001-29','RT003',0,8000,22,0,0,0,176000);
/*!40000 ALTER TABLE `sales_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail_tax`
--

DROP TABLE IF EXISTS `sales_detail_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `PRODUCT_DISC1` double DEFAULT '0',
  `PRODUCT_DISC2` double DEFAULT '0',
  `PRODUCT_DISC_RP` double DEFAULT '0',
  `SALES_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail_tax`
--

LOCK TABLES `sales_detail_tax` WRITE;
/*!40000 ALTER TABLE `sales_detail_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_detail_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_header`
--

DROP TABLE IF EXISTS `sales_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SALES_DATE` datetime DEFAULT NULL,
  `SALES_TOTAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SALES_TOP` tinyint(3) unsigned DEFAULT '0',
  `SALES_TOP_DATE` date DEFAULT NULL,
  `SALES_PAID` tinyint(3) unsigned DEFAULT '0',
  `SALES_PAYMENT` double unsigned DEFAULT '0',
  `SALES_PAYMENT_CHANGE` double unsigned DEFAULT '0',
  `SALES_PAYMENT_METHOD` tinyint(3) unsigned DEFAULT '0',
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `SALES_VOID` tinyint(3) DEFAULT '0',
  `SALES_ACTIVE` tinyint(3) DEFAULT '1',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header`
--

LOCK TABLES `sales_header` WRITE;
/*!40000 ALTER TABLE `sales_header` DISABLE KEYS */;
INSERT INTO `sales_header` VALUES (11,'SLO001-9',1,'2016-11-18 12:34:00',2000,0,1,'2016-11-18',0,1000,0,0,NULL,0,1),(12,'SLO001-10',1,'2016-11-18 12:42:00',2000,0,1,'2016-11-18',0,1000,0,0,'SQ-14',0,1),(13,'SLO001-11',1,'2016-11-18 12:53:00',10000,500,1,'2016-11-18',0,2000,0,0,'SQ-15',0,1),(14,'SLO001-12',1,'2016-11-18 13:06:00',16000,0,1,'2016-11-18',0,1000,0,0,'SQ-16',0,1),(15,'SLO001-13',1,'2016-11-18 14:04:00',2000,0,1,'2016-11-18',0,1000,0,0,'SQ-23',0,1),(16,'SLO001-14',1,'2016-11-18 14:06:00',10000,0,1,'2016-11-18',0,1000,0,0,NULL,0,1),(17,'SLO001-15',1,'2016-11-18 14:12:00',10000,0,1,'2016-11-18',0,1000,0,0,'SQ-22',0,1),(18,'SLO001-16',1,'2016-11-18 14:22:00',10000,0,1,'2016-11-18',0,6078,0,0,'SQ-24',0,1),(20,'SLO001-18',1,'2016-11-18 14:55:00',2000,0,1,'2016-11-18',0,1000,0,0,'SQ-26',0,1),(21,'SLO001-19',1,'2016-11-18 15:17:00',2000,0,1,'2016-11-18',0,100,0,0,NULL,0,1),(22,'SLO001-20',1,'2016-11-18 15:22:00',2000,0,1,'2016-11-18',0,100,0,0,NULL,0,1),(23,'SLO001-21',1,'2016-11-18 15:27:00',2000,0,1,'2016-11-18',0,100,0,0,'SQ-27',0,1),(24,'SLO001-22',1,'2016-11-18 15:33:00',2000,0,1,'2016-11-18',0,100,0,0,'SQ-27',0,1),(26,'SLO001-24',1,'2016-11-30 11:43:00',2000,0,1,'2016-11-30',0,3000,1000,0,'SQ-28',0,1),(27,'SLO001-25',1,'2016-11-29 22:19:00',20000,0,1,'2016-11-29',0,30000,10000,0,NULL,0,1),(28,'SLO001-26',1,'2016-12-02 22:26:00',110000,10000,1,'2016-12-02',0,101000,1000,0,'SQ-32',0,1),(30,'SLO001-28',2,'2017-01-20 15:55:00',20000,0,1,'2017-01-20',0,10000,0,0,'SQ-37',0,1),(31,'SLO001-29',2,'2017-05-01 12:17:00',189000,100,1,'2017-05-01',1,200000,11100,0,NULL,0,1);
/*!40000 ALTER TABLE `sales_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_header_tax`
--

DROP TABLE IF EXISTS `sales_header_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_header_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SALES_DATE` datetime DEFAULT NULL,
  `SALES_TOTAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SALES_TOP` tinyint(3) unsigned DEFAULT '0',
  `SALES_TOP_DATE` date DEFAULT NULL,
  `SALES_PAID` tinyint(3) unsigned DEFAULT '0',
  `SALES_PAYMENT` double unsigned DEFAULT '0',
  `SALES_PAYMENT_CHANGE` double unsigned DEFAULT '0',
  `SALES_PAYMENT_METHOD` tinyint(3) unsigned DEFAULT '0',
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `ORIGIN_SALES_INVOICE` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `SALES_INVOICE_UNIQUE` (`SALES_INVOICE`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header_tax`
--

LOCK TABLES `sales_header_tax` WRITE;
/*!40000 ALTER TABLE `sales_header_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_header_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_quotation_detail`
--

DROP TABLE IF EXISTS `sales_quotation_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_quotation_detail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `SQ_SUBTOTAL` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_quotation_detail`
--

LOCK TABLES `sales_quotation_detail` WRITE;
/*!40000 ALTER TABLE `sales_quotation_detail` DISABLE KEYS */;
INSERT INTO `sales_quotation_detail` VALUES (1,'SQ-1','RT001',2000,2,4000),(2,'SQ-2','RT001',1000,2,2000),(3,'SQ-3','RT001',1000,1,1000),(4,'SQ-4','RT002',5000,2,10000),(5,'SQ-5','RT003',8000,2,16000),(6,'SQ-6','RT003',8000,1,8000),(7,'SQ-7','RT003',8000,1,8000),(8,'SQ-8','RT003',8000,1,8000),(9,'SQ-9','RT003',8000,1,8000),(10,'SQ-10','RT001',1000,2,2000),(12,'SQ-12','RT003',8000,2,16000),(13,'SQ-13','RT002',5000,2,10000),(15,'SQ-15','RT002',5000,2,10000),(17,'SQ-17','RT001',1000,2,2000),(18,'SQ-18','RT001',1000,2,2000),(19,'SQ-19','RT003',8000,2,16000),(20,'SQ-20','RT002',5000,3,15000),(21,'SQ-21','RT002',5000,2,10000),(22,'SQ-22','RT002',5000,2,10000),(23,'SQ-23','RT001',1000,2,2000),(24,'SQ-24','RT002',5000,2,10000),(25,'SQ-25','RT001',1000,2,2000),(26,'SQ-26','RT001',1000,2,2000),(27,'SQ-27','RT001',1000,2,2000),(30,'SQ-28','RT001',1000,2,2000),(31,'SQ-29','RT001',1000,2,2000),(32,'SQ-29','RT002',5000,3,15000),(33,'SQ-30','RT001',1000,2,2000),(36,'SQ-32','RT002',5000,22,110000),(37,'SQ-33','RT001',1000,2,2000),(39,'SQ-34','RT002',5000,22,110000),(43,'SQ-35','RT001',1000,22,22000),(44,'SQ-35','RT002',5000,2,10000),(45,'SQ-35','RT003',8000,4,32000),(47,'SQ-31','RT001',1000,2,2000),(52,'SQ-36','RT001',1000,2,2000),(54,'SQ-37','4.1',2500,8,20000),(56,'SQ-38','RT004',1000,20,20000);
/*!40000 ALTER TABLE `sales_quotation_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_quotation_header`
--

DROP TABLE IF EXISTS `sales_quotation_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_quotation_header` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SQ_INVOICE` varchar(30) DEFAULT NULL,
  `CUSTOMER_ID` smallint(5) unsigned DEFAULT '0',
  `SQ_DATE` datetime DEFAULT NULL,
  `SQ_TOTAL` double DEFAULT '0',
  `SALES_DISCOUNT_FINAL` double DEFAULT '0',
  `SQ_TOP` tinyint(3) unsigned DEFAULT '0',
  `SQ_TOP_DATE` date DEFAULT NULL,
  `SQ_APPROVED` tinyint(3) unsigned DEFAULT '0',
  `SQ_APPROVED_DATE` date DEFAULT NULL,
  `SALESPERSON_ID` tinyint(3) DEFAULT '0',
  `SQ_ORDER_DATE` date DEFAULT NULL,
  `SQ_DP` double DEFAULT '0',
  `SQ_ORDER_ADDRESS` varchar(100) DEFAULT '',
  `DELIVERED` tinyint(3) DEFAULT '0',
  `COMPLETED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `SQ_INVOICE_UNIQUE` (`SQ_INVOICE`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_quotation_header`
--

LOCK TABLES `sales_quotation_header` WRITE;
/*!40000 ALTER TABLE `sales_quotation_header` DISABLE KEYS */;
INSERT INTO `sales_quotation_header` VALUES (1,'SQ-1',0,'2016-11-14 13:47:00',4000,100,1,'2016-11-14',1,'2016-11-15',1,NULL,0,'',0,0),(2,'SQ-2',1,'2016-11-15 15:06:00',2000,0,1,'2016-11-15',0,NULL,1,'2016-11-17',0,'',0,0),(3,'SQ-3',1,'2016-11-16 14:37:00',1000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',500,'',0,0),(4,'SQ-4',1,'2016-11-16 14:44:00',10000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',5000,'',1,0),(5,'SQ-5',1,'2016-11-16 15:19:00',16000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',10000,'aaaassssdddddd',0,0),(6,'SQ-6',1,'2016-11-16 15:21:00',8000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',1000,'00aasssddffgghhjj',0,0),(7,'SQ-7',1,'2016-11-16 15:33:00',8000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',2000,'',0,0),(8,'SQ-8',1,'2016-11-16 15:36:00',8000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',2000,'',0,0),(9,'SQ-9',1,'2016-11-16 15:40:00',8000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',1000,'',0,0),(10,'SQ-10',1,'2016-11-16 17:38:00',2000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',1000,'',0,0),(11,'SQ-11',1,'2016-11-16 17:59:00',8000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',0,'System.Windows.Forms.TextBox, Text: ',0,0),(12,'SQ-12',1,'2016-11-16 18:03:00',16000,0,1,'2016-11-16',0,NULL,1,'2016-11-16',5000,'assadasdsad',0,0),(13,'SQ-13',1,'2016-11-18 00:59:00',10000,10,1,'2016-11-18',0,NULL,1,'2016-11-19',100,'sddds',0,0),(14,'SQ-14',1,'2016-11-18 11:31:00',2000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',1000,'System.Windows.Forms.TextBox, Text: toko serba ada ',1,0),(15,'SQ-15',1,'2016-11-18 12:52:00',10000,500,1,'2016-11-18',1,'2016-11-18',1,'2016-11-19',2000,'hhhhhh',1,0),(16,'SQ-16',1,'2016-11-18 13:05:00',16000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-24',1000,'aaaaa',1,0),(17,'SQ-17',1,'2016-11-18 13:47:00',2000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',1500,'aaaaa',1,0),(18,'SQ-18',1,'2016-11-18 13:49:00',2000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',200,'',0,0),(19,'SQ-19',1,'2016-11-18 13:50:00',16000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',1000,'aaaaaa',1,0),(20,'SQ-20',1,'2016-11-18 13:52:00',15000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',10000,'',0,0),(21,'SQ-21',1,'2016-11-18 13:58:00',10000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',1000,'',0,0),(22,'SQ-22',1,'2016-11-18 13:59:00',10000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',1000,'',0,0),(23,'SQ-23',1,'2016-11-18 14:04:00',2000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',1000,'',0,0),(24,'SQ-24',1,'2016-11-18 14:22:00',10000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',6078,'',0,0),(25,'SQ-25',1,'2016-11-18 14:38:00',2000,0,1,'2016-11-18',0,NULL,1,'2016-11-18',2000,'',0,0),(26,'SQ-26',1,'2016-11-18 14:46:00',2000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',1000,'',0,0),(27,'SQ-27',1,'2016-11-15 15:06:00',2000,0,1,'2016-11-18',1,'2016-11-18',1,'2016-11-18',100,'',0,0),(28,'SQ-28',1,'2016-11-25 12:06:00',2000,0,1,'2016-11-25',1,'2016-11-25',1,'2016-11-30',1000,'aaaaaaa',1,1),(29,'SQ-29',1,'2016-11-28 12:11:00',17000,0,0,'2016-11-30',0,NULL,1,'2016-11-30',1000,'jalan jalan pagi',1,0),(30,'SQ-30',1,'2016-11-30 12:11:00',2000,0,1,'2016-11-30',1,'2016-11-30',1,'2016-12-01',3000,'',0,0),(31,'SQ-31',1,'2016-12-01 12:43:00',2000,0,1,'2016-12-01',0,NULL,1,'2016-12-02',1000,'',0,0),(32,'SQ-32',1,'2016-12-02 22:26:00',110000,10000,1,'2016-12-02',1,'2016-12-02',1,'2016-12-05',0,'jalan jalan pagi siang sekali',1,1),(33,'SQ-33',1,'2016-12-02 22:41:00',2000,0,1,'2016-12-02',0,NULL,1,'2016-12-02',0,'',0,0),(34,'SQ-34',1,'2016-12-02 22:47:00',110000,15000,1,'2016-12-02',1,'2016-12-02',1,'2016-12-02',20000,'',0,0),(35,'SQ-35',1,'2016-12-06 12:05:00',64000,0,1,'2016-12-06',0,NULL,1,'2016-12-06',20000,'',0,0),(36,'SQ-36',1,'2016-12-07 23:10:00',2000,0,1,'2016-12-07',0,NULL,1,'2016-12-07',1000,'',0,0),(37,'SQ-37',2,'2017-01-20 15:55:00',20000,0,1,'2017-01-20',1,'2017-01-20',1,'2017-01-26',10000,'',0,1),(38,'SQ-38',1,'2017-05-01 12:20:00',20000,0,1,'2017-05-01',1,'2017-05-01',1,'2017-05-10',1000,'',0,0);
/*!40000 ALTER TABLE `sales_quotation_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_config`
--

DROP TABLE IF EXISTS `sys_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_config` (
  `id` tinyint(3) unsigned NOT NULL,
  `no_faktur` varchar(30) NOT NULL DEFAULT '',
  `branch_id` tinyint(3) unsigned DEFAULT NULL,
  `HQ_IP4` varchar(15) DEFAULT NULL,
  `store_name` varchar(50) DEFAULT NULL,
  `store_address` varchar(100) DEFAULT NULL,
  `store_phone` varchar(20) DEFAULT NULL,
  `store_email` varchar(50) DEFAULT NULL,
  `quotation_reminder` tinyint(3) DEFAULT NULL,
  `server_IP4` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_config`
--

LOCK TABLES `sys_config` WRITE;
/*!40000 ALTER TABLE `sys_config` DISABLE KEYS */;
INSERT INTO `sys_config` VALUES (1,'SLO001',0,'127.0.0.1',NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `sys_config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sys_config_tax`
--

DROP TABLE IF EXISTS `sys_config_tax`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sys_config_tax` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PERSENTASE_PENJUALAN` int(11) DEFAULT '0',
  `PERSENTASE_PEMBELIAN` int(11) DEFAULT '0',
  `AVERAGE_PENJUALAN_HARIAN` double DEFAULT '0',
  `AVERAGE_PEMBELIAN_HARIAN` double DEFAULT '0',
  `RASIO_TOLERANSI` double DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sys_config_tax`
--

LOCK TABLES `sys_config_tax` WRITE;
/*!40000 ALTER TABLE `sys_config_tax` DISABLE KEYS */;
/*!40000 ALTER TABLE `sys_config_tax` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temp_master_product`
--

DROP TABLE IF EXISTS `temp_master_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `temp_master_product` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_BARCODE` int(10) unsigned DEFAULT NULL,
  `PRODUCT_NAME` varchar(50) DEFAULT NULL,
  `PRODUCT_DESCRIPTION` varchar(100) DEFAULT NULL,
  `PRODUCT_BASE_PRICE` double DEFAULT NULL,
  `PRODUCT_RETAIL_PRICE` double DEFAULT NULL,
  `PRODUCT_BULK_PRICE` double DEFAULT NULL,
  `PRODUCT_WHOLESALE_PRICE` double DEFAULT NULL,
  `UNIT_ID` smallint(5) unsigned DEFAULT '0',
  `PRODUCT_IS_SERVICE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `PRODUCT_ID_UNIQUE` (`PRODUCT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temp_master_product`
--

LOCK TABLES `temp_master_product` WRITE;
/*!40000 ALTER TABLE `temp_master_product` DISABLE KEYS */;
/*!40000 ALTER TABLE `temp_master_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `temp_product_category`
--

DROP TABLE IF EXISTS `temp_product_category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `temp_product_category` (
  `PRODUCT_ID` varchar(50) NOT NULL,
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`PRODUCT_ID`,`CATEGORY_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `temp_product_category`
--

LOCK TABLES `temp_product_category` WRITE;
/*!40000 ALTER TABLE `temp_product_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `temp_product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unit_convert`
--

DROP TABLE IF EXISTS `unit_convert`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `unit_convert` (
  `CONVERT_UNIT_ID_1` smallint(5) unsigned NOT NULL,
  `CONVERT_UNIT_ID_2` smallint(5) unsigned NOT NULL,
  `CONVERT_MULTIPLIER` float DEFAULT NULL,
  PRIMARY KEY (`CONVERT_UNIT_ID_1`,`CONVERT_UNIT_ID_2`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unit_convert`
--

LOCK TABLES `unit_convert` WRITE;
/*!40000 ALTER TABLE `unit_convert` DISABLE KEYS */;
/*!40000 ALTER TABLE `unit_convert` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_access_management`
--

DROP TABLE IF EXISTS `user_access_management`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_access_management` (
  `ID` smallint(5) unsigned NOT NULL AUTO_INCREMENT,
  `GROUP_ID` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(5) unsigned DEFAULT NULL,
  `USER_ACCESS_OPTION` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_access_management`
--

LOCK TABLES `user_access_management` WRITE;
/*!40000 ALTER TABLE `user_access_management` DISABLE KEYS */;
INSERT INTO `user_access_management` VALUES (1,1,1,1),(2,1,2,1),(3,1,3,6),(4,1,4,6),(5,1,5,1),(6,1,6,1),(7,1,7,1),(8,1,8,1),(9,1,9,1),(10,1,10,6),(11,1,11,1),(12,1,12,1),(13,1,13,1),(14,1,14,1),(15,1,15,1),(16,1,16,6),(17,1,17,1),(18,1,18,1),(19,1,19,1),(20,1,20,1),(21,1,21,1),(22,1,22,1),(23,1,23,6),(24,1,24,1),(25,1,25,1),(26,1,26,1),(27,1,27,1),(28,1,28,1),(29,1,29,1),(30,1,30,0),(31,1,31,0),(32,1,32,0),(33,1,33,1),(34,1,34,0),(35,1,35,1),(36,1,36,6),(37,1,37,1),(38,1,38,1),(39,1,39,1),(40,1,40,1),(41,1,41,1),(42,1,42,1),(43,1,43,6),(44,1,44,1),(45,1,45,1),(46,1,46,1),(47,1,47,1),(48,1,48,1),(49,2,1,0),(50,2,2,0),(51,2,3,0),(52,2,4,0),(53,2,5,0),(54,2,6,0),(55,2,7,0),(56,2,8,0),(57,2,9,0),(58,2,10,0),(59,2,11,0),(60,2,12,0),(61,2,13,0),(62,2,14,0),(63,2,15,0),(64,2,16,0),(65,2,17,0),(66,2,18,0),(67,2,19,0),(68,2,20,0),(69,2,21,0),(70,2,22,0),(71,2,23,0),(72,2,24,0),(73,2,25,0),(74,2,26,0),(75,2,27,0),(76,2,28,0),(77,2,29,0),(78,2,30,0),(79,2,31,0),(80,2,32,0),(81,2,33,0),(82,2,34,0),(83,2,35,1),(84,2,36,6),(85,2,37,1),(86,2,38,0),(87,2,39,1),(88,2,40,1),(89,2,41,1),(90,2,42,1),(91,2,43,0),(92,2,44,1),(93,2,45,1),(94,2,46,1),(95,2,47,0),(96,2,48,0),(97,1,49,1),(98,2,49,0),(99,1,50,1),(100,1,51,1),(101,1,52,1),(102,1,53,1),(103,1,54,1),(104,1,55,1),(105,1,56,1);
/*!40000 ALTER TABLE `user_access_management` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user_change_log`
--

DROP TABLE IF EXISTS `user_change_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_change_log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `USER_ID` tinyint(3) unsigned DEFAULT NULL,
  `MODULE_ID` smallint(6) DEFAULT NULL,
  `CHANGE_ID` tinyint(3) unsigned DEFAULT NULL,
  `CHANGE_DATETIME` datetime DEFAULT NULL,
  `CHANGE_DESCRIPTION` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_change_log`
--

LOCK TABLES `user_change_log` WRITE;
/*!40000 ALTER TABLE `user_change_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_change_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-01 20:48:27
