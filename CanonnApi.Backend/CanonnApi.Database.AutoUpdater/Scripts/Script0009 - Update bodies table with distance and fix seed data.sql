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

SET @newVersion = 9;

IF @version < @newVersion THEN

	-- START MIGRATION SCRIPT

ALTER TABLE `body`
	ADD COLUMN `distance` INT(11) NULL DEFAULT NULL COMMENT 'distance of the body to system arrival point'
	AFTER `name`;

UPDATE `body`
	SET `name` = 'CD 1'
	WHERE `id` = 8;

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
