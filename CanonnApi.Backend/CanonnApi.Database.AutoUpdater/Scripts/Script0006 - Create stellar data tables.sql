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

SET @newVersion = 6;

IF @version < @newVersion THEN
	-- START MIGRATION SCRIPT

CREATE TABLE `system` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the system entry',
	`name` VARCHAR(150) NOT NULL COMMENT 'name of the system',
	`edsm_ext_id` INT(11) NULL DEFAULT NULL COMMENT 'external id in elite dangerous star map (EDSM) API',
	`eddb_ext_id` INT(11) NULL DEFAULT NULL COMMENT 'external id in elite dangerous db (EDDB.io)',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_name` (`name`)
)
COMMENT='Holds data about the systems that are relevant for the canonn API'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

CREATE TABLE `body` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the system entry',
	`system_id` INT(11) NOT NULL COMMENT 'id of the system',
	`name` VARCHAR(150) NOT NULL COMMENT 'name of the body',
	`edsm_ext_id` INT(11) NULL DEFAULT NULL COMMENT 'external id in elite dangerous star map (EDSM) API',
	`eddb_ext_id` INT(11) NULL DEFAULT NULL COMMENT 'external id in elite dangerous db (EDDB.io)',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE INDEX `UX_body` (`system_id`, `name`),
	INDEX `FK_body_system` (`system_id`),
	CONSTRAINT `FK_body_system` FOREIGN KEY (`system_id`) REFERENCES `system` (`id`)
)
COMMENT='Holds data about the bodies that are relevant for the canonn API'
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
