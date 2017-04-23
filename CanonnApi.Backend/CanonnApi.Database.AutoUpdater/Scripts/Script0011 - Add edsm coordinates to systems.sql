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

SET @newVersion = 11;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

ALTER TABLE `system`
	ADD COLUMN `edsm_coord_x` FLOAT NULL COMMENT 'the edsm x coordinate of this system'
	AFTER `name`,
	ADD COLUMN `edsm_coord_y` FLOAT NULL COMMENT 'the edsm y coordinate of this system'
	AFTER `edsm_coord_x`,
	ADD COLUMN `edsm_coord_z` FLOAT NULL COMMENT 'the edsm z coordinate of this system'
	AFTER `edsm_coord_y`,
	ADD COLUMN `edsm_coord_updated` DATETIME NULL COMMENT 'specifies when the edsm coordinates have been updated the last time'
	AFTER `edsm_coord_z`;

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
