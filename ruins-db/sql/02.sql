/*
 ruins db version 2
 creates tables:
 - ruin_type
 - ruin_layout
 - obelisk_group
 - obelisk
 - ruinlayout_obeliskgroups
 - layout_variant
 - active_obelisk
 
 inserts initial data
 - ruin_type
 
*/

CREATE TABLE `ruin_type` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the ruin type',
	`name` TEXT NOT NULL COMMENT 'name of the ruin type',
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
	`type_id` INT(11) NOT NULL COMMENT 'ruin type on which this group can be found',
	`name` MEDIUMTEXT NOT NULL COMMENT 'name of the ruin group',
	`count` INT(11) NOT NULL COMMENT 'amount of obelisks in this group',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `FK_obeliskgroup_ruintype` (`type_id`),
	CONSTRAINT `FK_obeliskgroup_ruintype` FOREIGN KEY (`type_id`) REFERENCES `ruin_type` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='Holds information about the available obelisk groups for a certain ruin site type'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `obelisk` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the obelisk',
	`group_id` INT(11) NOT NULL COMMENT 'the group where this obelisk can be found',
	`number` INT(11) NOT NULL COMMENT 'number of this obelisk within the group',
	`relict_id` INT(11) NULL DEFAULT NULL COMMENT 'relict required to scan this obelisk',
	`data_id` INT(11) NULL DEFAULT NULL COMMENT 'the codex data that can be retrieved from this obelisk',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_obelisk` (`group_id`, `number`),
	INDEX `FK_obelisk_relict` (`relict_id`),
	INDEX `FK_obelisk_data` (`data_id`),
	CONSTRAINT `FK_obelisk_data` FOREIGN KEY (`data_id`) REFERENCES `codex_data` (`id`) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT `FK_obelisk_group` FOREIGN KEY (`group_id`) REFERENCES `obelisk_group` (`id`) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT `FK_obelisk_relict` FOREIGN KEY (`relict_id`) REFERENCES `relict` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='defines an individual obelisk in a certain group'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `ruin_layout` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the layout',
	`type_id` INT(11) NOT NULL COMMENT 'the ruin type this layout is for',
	`name` VARCHAR(50) NOT NULL COMMENT 'name of this layout',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_ruinlayout` (`type_id`, `name`),
	CONSTRAINT `FK_ruinlayout_ruintype` FOREIGN KEY (`type_id`) REFERENCES `ruin_type` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='stores the different layouts for the ruins'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `ruinlayout_obeilskgroups` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for this mapping',
	`layout_id` INT(11) NOT NULL COMMENT 'the layout',
	`group_id` INT(11) NOT NULL COMMENT 'the group',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_ruinlayout_obeliskgroups` (`layout_id`, `group_id`),
	INDEX `FK_ruinlayoutobeliskgroups_obeliskgroup` (`group_id`),
	CONSTRAINT `FK_ruinlayoutobeliskgroups_obeliskgroup` FOREIGN KEY (`group_id`) REFERENCES `obelisk_group` (`id`) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT `FK_ruinlayoutobeliskgroups_ruinlayout` FOREIGN KEY (`layout_id`) REFERENCES `ruin_layout` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='maps the available obelisk groups to the layouts'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `layout_variant` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the variant',
	`layout_id` INT(11) NOT NULL COMMENT 'the layout for which this is a variant',
	`name` VARCHAR(50) NOT NULL COMMENT 'the name of this variant',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_layout_variant` (`layout_id`, `name`),
	CONSTRAINT `FK_layoutvariant_ruinlayout` FOREIGN KEY (`layout_id`) REFERENCES `ruin_layout` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='defines different variants (in active / broken obelisks) for a specific ruin type layout'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `active_obelisk` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of this assignment',
	`obelisk_id` INT(11) NOT NULL COMMENT 'id of the obelisk',
	`variant_id` INT(11) NOT NULL COMMENT 'id of the variant',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_active_obelisk` (`obelisk_id`, `variant_id`),
	INDEX `FK_activeobelisk_variant` (`variant_id`),
	CONSTRAINT `FK_activeobelisk_obelisk` FOREIGN KEY (`obelisk_id`) REFERENCES `obelisk` (`id`) ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT `FK_activeobelisk_variant` FOREIGN KEY (`variant_id`) REFERENCES `layout_variant` (`id`) ON UPDATE CASCADE ON DELETE CASCADE
)
COMMENT='Defines which obelisks are active on a specific layout variant'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;


INSERT INTO `ruin_type` (`id`, `name`) VALUES
	(1, 'Alpha'),
	(2, 'Beta'),
	(3, 'Gamma');

INSERT INTO `obelisk_group` (`id`, `type_id`, `name`, `count`) VALUES
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
	(16, 1, 'P', 13);