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

SET @newVersion = 14;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT


ALTER TABLE `body`
	CHANGE COLUMN `distance` `distance` INT(11) NULL DEFAULT NULL COMMENT 'distance of the body to system arrival point (from edsm)' AFTER `name`,
	ADD COLUMN `is_landable` BIT NULL DEFAULT NULL COMMENT 'is this body landable (from edsm)' AFTER `distance`,
	ADD COLUMN `gravity` DOUBLE NULL DEFAULT NULL COMMENT 'the gravity of this body (from edsm)' AFTER `is_landable`,
	ADD COLUMN `earth_masses` DOUBLE NULL DEFAULT NULL COMMENT 'the mass of this system compared to earth (from edsm)' AFTER `gravity`,
	ADD COLUMN `radius` DOUBLE NULL DEFAULT NULL COMMENT 'radius of this body (from edsm)' AFTER `earth_masses`,
	ADD COLUMN `edsm_last_update` DATETIME NULL DEFAULT NULL COMMENT 'when this entry was last updated from edsm' AFTER `eddb_ext_id`;


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
