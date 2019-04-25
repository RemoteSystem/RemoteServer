/*
Navicat MySQL Data Transfer

Source Server         : aliyun-remote
Source Server Version : 50639
Source Host           : 120.79.244.32:3306
Source Database       : test

Target Server Type    : MYSQL
Target Server Version : 50639
File Encoding         : 65001

Date: 2019-03-24 10:55:36
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `bio_fault`
-- ----------------------------
DROP TABLE IF EXISTS `bio_fault`;
CREATE TABLE `bio_fault` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `code` varchar(100) COLLATE utf8mb4_bin DEFAULT NULL,
  `dttime` timestamp NULL DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Table structure for `bio_item`
-- ----------------------------
DROP TABLE IF EXISTS `bio_item`;
CREATE TABLE `bio_item` (
  `num` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `blank_time` int(11) DEFAULT NULL,
  `calibration_method` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `corrected_intercept` float(10,2) DEFAULT NULL,
  `corrected_slope` float(10,2) DEFAULT NULL,
  `first_reagent_volume` float(10,2) DEFAULT NULL,
  `k_factor_value` float(10,2) DEFAULT NULL,
  `main_wavelength` int(11) DEFAULT NULL,
  `measuring_method` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `reaction_direction` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `reaction_time` int(11) DEFAULT NULL,
  `sample_volume` float(10,2) DEFAULT NULL,
  `second_reagent_volume` float(10,2) DEFAULT NULL,
  `sub_wavelength` int(11) DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`num`,`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Table structure for `bio_statistics`
-- ----------------------------
DROP TABLE IF EXISTS `bio_statistics`;
CREATE TABLE `bio_statistics` (
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `sample` int(11) DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Table structure for `bio_statistics_item`
-- ----------------------------
DROP TABLE IF EXISTS `bio_statistics_item`;
CREATE TABLE `bio_statistics_item` (
  `num` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `R1` float(10,2) DEFAULT NULL,
  `R2` float(10,2) DEFAULT NULL,
  `smpl` int(10) DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`num`,`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;


-- ----------------------------
-- Table structure for `dump`
-- ----------------------------
DROP TABLE IF EXISTS `dump`;
CREATE TABLE `dump` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `encoding` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `filename` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `data` mediumtext COLLATE utf8mb4_bin,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Table structure for `user`
-- ----------------------------
DROP TABLE IF EXISTS `user`;
CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userName` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `password` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `name` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `sex` int(1) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `birthday` date DEFAULT NULL,
  `idCard` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `dtInsert` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  `isAdmin` tinyint(1) DEFAULT '0',
  `isDel` tinyint(1) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

-- ----------------------------
-- Table structure for `user_rights`
-- ----------------------------
DROP TABLE IF EXISTS `user_rights`;
CREATE TABLE `user_rights` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'id',
  `userId` int(11) NOT NULL,
  `rights` varchar(500) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;