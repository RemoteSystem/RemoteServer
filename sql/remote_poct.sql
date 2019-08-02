DROP TABLE IF EXISTS `poct_fault`;
CREATE TABLE `poct_fault` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `code` varchar(100) COLLATE utf8mb4_bin DEFAULT NULL,
  `dttime` timestamp NULL DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=771331 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

DROP TABLE IF EXISTS `poct_item`;
CREATE TABLE `poct_item` (
  `num` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `card_name` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `incubate_time` int(11) DEFAULT NULL,
  `analyte_1` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `analyte_2` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `analyte_3` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `signals` int(11) DEFAULT NULL,
  `card_lot` varchar(50) COLLATE utf8mb4_bin DEFAULT NULL,
  `expiry` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `analyte_1_params` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `analyte_2_params` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `analyte_3_params` varchar(200) COLLATE utf8mb4_bin DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`num`,`card_lot`,`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

DROP TABLE IF EXISTS `poct_statistics`;
CREATE TABLE `poct_statistics` (
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `sample` int(11) DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

DROP TABLE IF EXISTS `poct_statistics_item`;
CREATE TABLE `poct_statistics_item` (
  `num` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `device_sn` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `smpl` int(10) DEFAULT NULL,
  `card_consume` int(10) DEFAULT NULL,
  `dtinsert` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`num`,`device_sn`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;