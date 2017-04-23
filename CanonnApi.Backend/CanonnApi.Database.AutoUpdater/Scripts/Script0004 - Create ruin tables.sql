-- due to a bug in DbUp, 'delimiter' may not start at the very first character in a line...
 delimiter $$

DROP PROCEDURE IF EXISTS `up`$$

CREATE PROCEDURE up()
BEGIN

SET @version := (
	SELECT `value`
	FROM `canonndb_metadata`
	WHERE `name` = 'version'
);

SET @newVersion = 4;

IF @version < @newVersion THEN
	-- START MIGRATION SCRIPT

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

	-- END MIGRATION SCRIPT

	-- update version and add metadata	
	INSERT INTO `canonndb_metadata` (`name`, `value`) VALUES
		(CONCAT('update_v', @newVersion), DATE_FORMAT(UTC_TIMESTAMP(), '%Y-%m-%dT%TZ'));
	UPDATE `canonndb_metadata` SET `value` = @newVersion WHERE `name` = 'version';

END IF;

END$$ -- PROCUDURE up()

 delimiter ;

CALL up();

DROP PROCEDURE `up`;
