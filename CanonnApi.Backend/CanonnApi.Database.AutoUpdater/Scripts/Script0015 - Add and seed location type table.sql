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

SET @newVersion = 15;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

CREATE TABLE `location_type` (
	`id` INT(11) NOT NULL AUTO_INCREMENT COMMENT 'id of the location type',
	`short_name` TEXT(10) NOT NULL COMMENT 'the short name of the location type',
	`name` TEXT(100) NOT NULL COMMENT 'the name of the location type',
	`is_surface` BIT NOT NULL COMMENT 'if the location type is on the surface of a body',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	INDEX `IX_location_short_name` (`short_name`(10))
)
COMMENT='holds the location types'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

INSERT INTO `location_type` (`id`, `short_name`, `name`, `is_surface`) VALUES
	(1, 'GS', 'Ancient Ruins', 1),
	(2, 'BA', 'Alien Structure (Barnacle)', 1),
	(3, 'US', 'Unknown Structure', 1),
	(4, 'AC', 'Alien Crash Site', 1),
	(5, 'UA USS', 'Unknown Artifact (Unidentified Signal Source, Threat 4 Anomaly)', 0),
	(6, 'UP USS', 'Unknown Probe (Unidentified Signal Source)', 0);


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
