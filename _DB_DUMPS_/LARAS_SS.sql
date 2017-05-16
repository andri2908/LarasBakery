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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_product`
--

LOCK TABLES `master_product` WRITE;
/*!40000 ALTER TABLE `master_product` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_customer`
--

DROP TABLE IF EXISTS `master_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_customer` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `CUSTOMER_ID` smallint(5) unsigned NOT NULL,
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
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_customer`
--

LOCK TABLES `master_customer` WRITE;
/*!40000 ALTER TABLE `master_customer` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_customer` ENABLE KEYS */;
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
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `CATEGORY_ID` tinyint(3) unsigned NOT NULL,
  `CATEGORY_NAME` varchar(50) DEFAULT NULL,
  `CATEGORY_DESCRIPTION` varchar(100) DEFAULT NULL,
  `CATEGORY_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_category`
--

LOCK TABLES `master_category` WRITE;
/*!40000 ALTER TABLE `master_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_category` ENABLE KEYS */;
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
INSERT INTO `master_module` VALUES (1,'MANAJEMEN SISTEM',NULL,1,1),(2,'DATABASE',NULL,1,1),(3,'MANAJEMEN USER',NULL,3,1),(4,'MANAJEMEN CABANG',NULL,3,1),(5,'SINKRONISASI INFORMASI',NULL,1,1),(6,'PENGATURAN PRINTER',NULL,1,1),(7,'PENGATURAN GAMBAR LATAR',NULL,1,1),(8,'GUDANG',NULL,1,1),(9,'PRODUK',NULL,1,1),(10,'TAMBAH / UPDATE PRODUK',NULL,3,1),(11,'PENGATURAN HARGA PRODUK',NULL,1,1),(12,'PENGATURAN LIMIT STOK',NULL,1,1),(13,'PENGATURAN KATEGORI PRODUK',NULL,1,1),(14,'PECAH SATUAN PRODUK',NULL,1,1),(15,'PENGATURAN NOMOR RAK',NULL,1,1),(16,'KATEGORI PRODUK',NULL,3,1),(17,'SATUAN PRODUK',NULL,1,1),(18,'TAMBAH / UPDATE SATUAN',NULL,1,1),(19,'PENGATURAN KONVERSI SATUAN',NULL,1,1),(20,'STOK OPNAME',NULL,1,1),(21,'PENYESUAIAN STOK',NULL,1,1),(22,'MUTASI BARANG',NULL,1,1),(23,'TAMBAH / UPDATE MUTASI BARANG',NULL,3,1),(24,'CEK PERMINTAAN BARANG',NULL,1,1),(25,'PENERIMAAN BARANG',NULL,1,1),(26,'PENERIMAAN BARANG DARI MUTASI',NULL,1,1),(27,'PENERIMAAN BARANG DARI PO',NULL,1,1),(28,'PEMBELIAN',NULL,1,1),(29,'SUPPLIER',NULL,1,1),(30,'REQUEST ORDER',NULL,3,1),(31,'PURCHASE ORDER',NULL,3,1),(32,'REPRINT REQUEST ORDER',NULL,1,1),(33,'RETUR PEMBELIAN KE SUPPLIER',NULL,1,1),(34,'RETUR PERMINTAAN KE PUSAT',NULL,1,1),(35,'PENJUALAN',NULL,1,1),(36,'PELANGGAN',NULL,3,1),(37,'TRANSAKSI PENJUALAN',NULL,1,1),(38,'SET NO FAKTUR',NULL,1,1),(39,'RETUR PENJUALAN',NULL,1,1),(40,'RETUR PENJUALAN BY INVOICE',NULL,1,1),(41,'RETUR PENJUALAN BY STOK',NULL,1,1),(42,'KEUANGAN',NULL,1,1),(43,'PENGATURAN NO AKUN',NULL,3,1),(44,'TRANSAKSI',NULL,1,1),(45,'TAMBAH TRANSAKSI HARIAN',NULL,1,1),(46,'PEMBAYARAN PIUTANG',NULL,1,1),(47,'PEMBAYARAN PIUTANG MUTASI',NULL,1,1),(48,'PEMBAYARAN HUTANG KE SUPPLIER',NULL,1,1),(49,'PENGATURAN LIMIT PAJAK',NULL,1,1),(50,'MODUL MESSAGING',NULL,1,1),(51,'TAX_MODULE',NULL,1,1),(52,'SALES QUOTATION',NULL,1,0),(53,'APPROVAL SALES QUOTATION',NULL,1,0),(54,'PENGATURAN HARGA POKOK',NULL,1,1),(55,'REVISI STOCK TAKE HARIAN',NULL,1,1),(56,'SALES QUOTATION',NULL,1,0);
/*!40000 ALTER TABLE `master_module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_supplier`
--

DROP TABLE IF EXISTS `master_supplier`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_supplier` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `SUPPLIER_ID` smallint(5) unsigned NOT NULL,
  `SUPPLIER_FULL_NAME` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS1` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS2` varchar(50) DEFAULT NULL,
  `SUPPLIER_ADDRESS_CITY` varchar(50) DEFAULT NULL,
  `SUPPLIER_PHONE` varchar(15) DEFAULT NULL,
  `SUPPLIER_FAX` varchar(15) DEFAULT NULL,
  `SUPPLIER_EMAIL` varchar(50) DEFAULT NULL,
  `SUPPLIER_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_supplier`
--

LOCK TABLES `master_supplier` WRITE;
/*!40000 ALTER TABLE `master_supplier` DISABLE KEYS */;
/*!40000 ALTER TABLE `master_supplier` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `master_unit`
--

DROP TABLE IF EXISTS `master_unit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `master_unit` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `UNIT_ID` smallint(5) unsigned NOT NULL,
  `UNIT_NAME` varchar(50) DEFAULT NULL,
  `UNIT_DESCRIPTION` varchar(100) DEFAULT NULL,
  `UNIT_ACTIVE` tinyint(3) unsigned DEFAULT NULL,
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `master_unit`
--

LOCK TABLES `master_unit` WRITE;
/*!40000 ALTER TABLE `master_unit` DISABLE KEYS */;
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
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `payment_id` int(10) unsigned NOT NULL,
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
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment_credit`
--

LOCK TABLES `payment_credit` WRITE;
/*!40000 ALTER TABLE `payment_credit` DISABLE KEYS */;
/*!40000 ALTER TABLE `payment_credit` ENABLE KEYS */;
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
INSERT INTO `product_category` VALUES ('RT001',1),('RT002',1);
/*!40000 ALTER TABLE `product_category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail`
--

DROP TABLE IF EXISTS `sales_detail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `ID` int(10) unsigned NOT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_PRICE` double DEFAULT '0',
  `PRODUCT_SALES_PRICE` double DEFAULT '0',
  `PRODUCT_QTY` double DEFAULT '0',
  `PRODUCT_DISC1` double DEFAULT '0',
  `PRODUCT_DISC2` double DEFAULT '0',
  `PRODUCT_DISC_RP` double DEFAULT '0',
  `SALES_SUBTOTAL` double DEFAULT '0',
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail`
--

LOCK TABLES `sales_detail` WRITE;
/*!40000 ALTER TABLE `sales_detail` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_detail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_detail_fulfillment`
--

DROP TABLE IF EXISTS `sales_detail_fulfillment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_detail_fulfillment` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `ID` int(10) unsigned NOT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `FULFILLMENT_DATE` datetime DEFAULT NULL,
  `PRODUCT_ID` varchar(50) DEFAULT NULL,
  `PRODUCT_QTY` double DEFAULT NULL,
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_detail_fulfillment`
--

LOCK TABLES `sales_detail_fulfillment` WRITE;
/*!40000 ALTER TABLE `sales_detail_fulfillment` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_detail_fulfillment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sales_header`
--

DROP TABLE IF EXISTS `sales_header`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sales_header` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `ID` int(10) unsigned NOT NULL,
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
  `SALES_ORDER_DATE` date DEFAULT NULL,
  `SALES_ORDER_DELIVERED` tinyint(3) DEFAULT '0',
  `SALES_ORDER_DELIVERED_ADDRESS` varchar(100) DEFAULT NULL,
  `SALES_ORDER_COMPLETED` tinyint(3) DEFAULT '0',
  `REFERENCE_SO` varchar(30) DEFAULT NULL,
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sales_header`
--

LOCK TABLES `sales_header` WRITE;
/*!40000 ALTER TABLE `sales_header` DISABLE KEYS */;
/*!40000 ALTER TABLE `sales_header` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credit`
--

DROP TABLE IF EXISTS `credit`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `credit` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `BRANCH_ID` tinyint(3) unsigned DEFAULT '0',
  `CREDIT_ID` int(10) unsigned NOT NULL,
  `SALES_INVOICE` varchar(30) DEFAULT NULL,
  `PM_INVOICE` varchar(30) DEFAULT NULL,
  `CREDIT_DUE_DATE` date DEFAULT NULL,
  `CREDIT_NOMINAL` double DEFAULT '0',
  `CREDIT_PAID` tinyint(3) unsigned DEFAULT NULL,
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',  
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credit`
--

LOCK TABLES `credit` WRITE;
/*!40000 ALTER TABLE `credit` DISABLE KEYS */;
/*!40000 ALTER TABLE `credit` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `daily_journal`
--

DROP TABLE IF EXISTS `daily_journal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `daily_journal` (
  `SYNC_ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `journal_id` int(10) unsigned NOT NULL,
  `account_id` smallint(5) unsigned NOT NULL,
  `journal_datetime` datetime NOT NULL,
  `journal_nominal` double NOT NULL,
  `branch_id` tinyint(3) unsigned DEFAULT NULL,
  `journal_description` varchar(100) DEFAULT NULL,
  `user_id` tinyint(3) unsigned NOT NULL,
  `pm_id` tinyint(3) unsigned NOT NULL,
  `SYNCHRONIZED` tinyint(3) DEFAULT '0',
  `EDITED` tinyint(3) DEFAULT '0',
  PRIMARY KEY (`SYNC_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `daily_journal`
--

LOCK TABLES `daily_journal` WRITE;
/*!40000 ALTER TABLE `daily_journal` DISABLE KEYS */;
/*!40000 ALTER TABLE `daily_journal` ENABLE KEYS */;
UNLOCK TABLES;