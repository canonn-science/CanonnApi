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

SET @newVersion = 12;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

ALTER TABLE `codex_data`
	ADD COLUMN `artifact_id` INT(11) NULL DEFAULT NULL COMMENT 'secondary relict required to gather this data entry' AFTER `entry_number`,
	ADD CONSTRAINT `FK_codexdata_artifact` FOREIGN KEY (`artifact_id`) REFERENCES `artifact` (`id`);

UPDATE `codex_data`
	SET `artifact_id` = (SELECT DISTINCT `artifact_id` FROM `obelisk` WHERE `codexdata_id` = `codex_data`.`id` AND `artifact_id` IS NOT NULL);

-- fix data that has multiple entries for a single data point
UPDATE `codex_data`
	SET `artifact_id` = NULL
	WHERE `id` IN (20, 75);
	
ALTER TABLE `obelisk`
	DROP FOREIGN KEY `FK_obelisk_artifact`,
	DROP COLUMN `artifact_id`;

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
