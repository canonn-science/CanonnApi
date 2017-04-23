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

SET @newVersion = 7;

IF @version < @newVersion THEN
	-- START MIGRATION SCRIPT

CREATE TABLE `ruin_site` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id for the site',
	`body_id` INT(11) NOT NULL COMMENT 'body the site is located on',
	`latitude` DECIMAL(6,4) NOT NULL COMMENT 'latitude coordinate of the site',
	`longitude` DECIMAL(7,4) NOT NULL COMMENT 'longitude coordinate of the site',
	`ruintype_id` INT(11) NOT NULL COMMENT 'id of the ruin type',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `FK_ruinsite_body` (`body_id`),
	INDEX `FK_ruinsite_ruintype` (`ruintype_id`),
	CONSTRAINT `FK_ruinsite_body` FOREIGN KEY (`body_id`) REFERENCES `body` (`id`),
	CONSTRAINT `FK_ruinsite_ruintype` FOREIGN KEY (`ruintype_id`) REFERENCES `ruin_type` (`id`)
)
COMMENT='describes a single site'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `ruinsite_obeliskgroups` (
	`ruinsite_id` INT(11) NOT NULL COMMENT 'id of the ruin site',
	`obeliskgroup_id` INT(11) NOT NULL COMMENT 'id of the obelisk group',
	PRIMARY KEY (`ruinsite_id`, `obeliskgroup_id`),
	INDEX `FK_ruinsiteobeliskgroup_obeliskgroup` (`obeliskgroup_id`),
	CONSTRAINT `FK_ruinsiteobeliskgroup_obeliskgroup` FOREIGN KEY (`obeliskgroup_id`) REFERENCES `obelisk_group` (`id`),
	CONSTRAINT `FK_ruinsiteobeliskgroup_ruinsite` FOREIGN KEY (`ruinsite_id`) REFERENCES `ruin_site` (`id`)
)
COMMENT='Maps ruin sites to obelisk groups'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `ruinsite_activeobelisks` (
	`ruinsite_id` INT(11) NOT NULL COMMENT 'id of the ruin site',
	`obelisk_id` INT(11) NOT NULL COMMENT 'id of the obelisk',
	PRIMARY KEY (`ruinsite_id`, `obelisk_id`),
	INDEX `FK_ruinsiteactiveobelisk_obelisk` (`obelisk_id`),
	CONSTRAINT `FK_ruinsiteactiveobelisk_obelisk` FOREIGN KEY (`obelisk_id`) REFERENCES `obelisk` (`id`),
	CONSTRAINT `FK_ruinsiteactiveobelisk_ruinsite` FOREIGN KEY (`ruinsite_id`) REFERENCES `ruin_site` (`id`)
)
COMMENT='Maps active obelisks for a given ruin site'
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
