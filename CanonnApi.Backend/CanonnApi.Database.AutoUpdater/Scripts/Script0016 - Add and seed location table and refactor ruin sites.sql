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

SET @newVersion = 16;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

-- first create location table
CREATE TABLE `location` (
	`id` INT NOT NULL AUTO_INCREMENT COMMENT 'the id of this location',
	`locationtype_id` INT NOT NULL COMMENT 'the type of this location',
	`is_visible` BIT NOT NULL DEFAULT 1 COMMENT 'determines whether this location should be visible for endpoints',
	`system_id` INT NOT NULL COMMENT 'the system this location is in',
	`body_id` INT NULL DEFAULT NULL COMMENT 'the optional body this location is on or refers to',
	`latitude` DECIMAL(6,4) NULL DEFAULT NULL COMMENT 'if on surface, the latitude coordinate of this location',
	`longitude` DECIMAL(7,4) NULL DEFAULT NULL COMMENT 'if on surface, the longitute coordinate of this location',
	`direction_system_id` INT NULL DEFAULT NULL COMMENT 'if this location is a a far-away-POI, the system to target when at the body location',
	`distance` INT NULL DEFAULT NULL COMMENT 'if this location is a far-away-POI, the distance to travel from the body in direction of the direction system',
	`created` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	`updated` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	CONSTRAINT `FK_location_locationtype` FOREIGN KEY (`locationtype_id`) REFERENCES `location_type` (`id`),
	CONSTRAINT `FK_location_system` FOREIGN KEY (`system_id`) REFERENCES `system` (`id`),
	CONSTRAINT `FK_location_body` FOREIGN KEY (`body_id`) REFERENCES `body` (`id`),
	CONSTRAINT `FK_location_directionsystem` FOREIGN KEY (`direction_system_id`) REFERENCES `system` (`id`)
)
COMMENT='tracks locations (or point of interests) in the galaxy'
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;

-- then add location id to ruin site and pre-fill the column
ALTER TABLE `ruin_site`
	ADD COLUMN `location_id` INT NOT NULL COMMENT 'id of the location this ruin site is at' AFTER `id`;

SET @locationIdSeed := 0;
UPDATE `ruin_site` SET `location_id` = (SELECT @locationIdSeed := @locationIdSeed + 1) ORDER BY `id`;

-- seed the data from the ruins to the location table
INSERT INTO `location` (`id`, `locationtype_id`, `is_visible`, `system_id`, `body_id`, `latitude`, `longitude`, `created`)
	SELECT
		`ruin_site`.`location_id` as `id`,
		1 as `locationtype_id`,
		(`ruin_site`.`id` < 99997) as `is_visible`,
		`body`.`system_id` as `system_id`,
		`ruin_site`.`body_id` as `body_id`,
		`ruin_site`.`latitude` as `latitude`,
		`ruin_site`.`longitude` as `longitude`,
		`ruin_site`.`created` as `created`
	FROM `ruin_site`
	JOIN `body` ON `ruin_site`.`body_id` = `body`.`id`
	ORDER BY `ruin_site`.`id`;

-- set FK on ruin_site table to location and drop redundant columns
ALTER TABLE `ruin_site`
	DROP COLUMN `body_id`,
	DROP COLUMN `latitude`,
	DROP COLUMN `longitude`,
	DROP FOREIGN KEY `FK_ruinsite_body`,
	ADD CONSTRAINT `FK_ruinsite_location` FOREIGN KEY (`location_id`) REFERENCES `location` (`id`);


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
