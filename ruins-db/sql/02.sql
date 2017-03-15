/*
 ruins db version 2
 creates tables:
 - ruin_type
 - ruin_layout
 - obelisk_group
 - obelisk

 
 inserts initial data
 - ruin_type
 - obelisk_group
 - obelisk (partial, without data points)
 
*/

CREATE TABLE `ruin_type` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the ruin type',
	`name` VARCHAR(50) NOT NULL COMMENT 'name of the ruin type',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`)
)
COMMENT='Holds information about the types of ruins found'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `obelisk_group` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the obelisk group',
	`ruintype_id` INT(11) NOT NULL COMMENT 'ruin type on which this group can be found',
	`name` VARCHAR(50) NOT NULL COMMENT 'name of the ruin group',
	`count` INT(11) NOT NULL COMMENT 'amount of obelisks in this group',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `FK_obeliskgroup_ruintype` (`ruintype_id`),
	CONSTRAINT `FK_obeliskgroup_ruintype` FOREIGN KEY (`ruintype_id`) REFERENCES `ruin_type` (`id`)
)
COMMENT='Holds information about the available obelisk groups for a certain ruin site type'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


CREATE TABLE `obelisk` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the obelisk',
	`obeliskgroup_id` INT(11) NOT NULL COMMENT 'the group where this obelisk can be found',
	`number` INT(11) NOT NULL COMMENT 'number of this obelisk within the group',
	`is_broken` BIT(1) NOT NULL DEFAULT b'0' COMMENT 'determines whether this obelisk is broken or not',
	`artifact_id` INT(11) NULL DEFAULT NULL COMMENT 'relict required to scan this obelisk',
	`codexdata_id` INT(11) NULL DEFAULT NULL COMMENT 'the codex data that can be retrieved from this obelisk',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_obeliskgroupid` (`obeliskgroup_id`, `number`),
	INDEX `FK_obelisk_artifact` (`artifact_id`),
	INDEX `FK_obelisk_codexdata` (`codexdata_id`),
	CONSTRAINT `FK_obelisk_artifact` FOREIGN KEY (`artifact_id`) REFERENCES `artifact` (`id`),
	CONSTRAINT `FK_obelisk_codexdata` FOREIGN KEY (`codexdata_id`) REFERENCES `codex_data` (`id`),
	CONSTRAINT `FK_obelisk_obeliskgroup` FOREIGN KEY (`obeliskgroup_id`) REFERENCES `obelisk_group` (`id`)
)
COMMENT='defines an individual obelisk in a certain group'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


INSERT INTO `ruin_type` (`id`, `name`) VALUES
	(1, 'Alpha'),
	(2, 'Beta'),
	(3, 'Gamma');

INSERT INTO `obelisk_group` (`id`, `ruintype_id`, `name`, `count`) VALUES
	-- Alpha ruins groups
	(1, 1, 'A', 20),
	(2, 1, 'B', 21),
	(3, 1, 'C', 14),
	(4, 1, 'D', 14),
	(5, 1, 'E', 10),
	(6, 1, 'F', 22),
	(7, 1, 'G', 4),
	(8, 1, 'H', 7),
	(9, 1, 'I', 20),
	(10, 1, 'J', 8),
	(11, 1, 'K', 22),
	(12, 1, 'L', 10),
	(13, 1, 'M', 24),
	(14, 1, 'N', 10),
	(15, 1, 'O', 20),
	(16, 1, 'P', 13),
	(17, 1, 'Q', 29),
	-- Beta ruin groups
	(18, 2, 'A', 10),
	(19, 2, 'B', 6),
	(20, 2, 'C', 25),
	(21, 2, 'D', 12),
	(22, 2, 'E', 27),
	(23, 2, 'F', 8),
	(24, 2, 'G', 18),
	(25, 2, 'H', 10),
	(26, 2, 'I', 12),
	(27, 2, 'J', 10),
	(28, 2, 'K', 27),
	(29, 2, 'L', 6),
	(30, 2, 'M', 3),
	(31, 2, 'N', 22),
	(32, 2, 'O', 3),
	(33, 2, 'P', 27),
	(34, 2, 'Q', 8),
	(35, 2, 'R', 27),
	(36, 2, 'S', 3),
	(37, 2, 'T', 18),
	(38, 2, 'U', 55),
	-- Gama ruin grups
	(39, 3, 'A', 27),
	(40, 3, 'B', 27),
	(41, 3, 'C', 15),
	(42, 3, 'D', 15),
	(43, 3, 'E', 10),
	(44, 3, 'F', 15),
	(45, 3, 'G', 14),
	(46, 3, 'H', 57),
	(47, 3, 'I', 18),
	(48, 3, 'J', 9),
	(49, 3, 'K', 15),
	(50, 3, 'L', 14),
	(51, 3, 'M', 14),
	(52, 3, 'N', 14),
	(53, 3, 'O', 8),
	(54, 3, 'P', 27),
	(55, 3, 'Q', 10),
	(56, 3, 'R', 27),
	(57, 3, 'S', 8);