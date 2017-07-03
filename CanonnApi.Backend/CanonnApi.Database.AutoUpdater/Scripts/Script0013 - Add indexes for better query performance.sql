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

SET @newVersion = 13;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

ALTER TABLE `ruin_type`
	ADD INDEX `IX_ruintype_name` (`name`);

ALTER TABLE `obelisk_group`
	ADD INDEX `IX_obeliskgroup_name` (`name`);

ALTER TABLE `obelisk`
	ADD INDEX `IX_obelisk_scandata` (`is_broken`, `codexdata_id`, `number`);



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
