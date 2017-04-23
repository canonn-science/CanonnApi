-- due to a bug in DbUp, 'delimiter' may not start at the very first character in a line...
 delimiter $$

DROP PROCEDURE IF EXISTS `up`$$

CREATE PROCEDURE up()
BEGIN

SET @initialversion = (
	SELECT `table_comment`
	FROM `information_schema`.`tables`
	WHERE `table_schema` = (SELECT DATABASE() FROM DUAL)
		AND `table_name` = 'canonndb_metadata'
	);

IF @initialversion IS NULL THEN
	CREATE TABLE `canonndb_metadata` (
		`name` VARCHAR(255) NOT NULL COMMENT 'Key for metadata entries',
		`value` VARCHAR(255) NOT NULL COMMENT 'Value for metadata entries',
		PRIMARY KEY (`name`)
	)
	COMMENT='Table that holds metadata information about this database'
	COLLATE='utf8_general_ci'
	ENGINE=InnoDB
	;

	INSERT INTO `canonndb_metadata` (`name`, `value`) VALUES
		('version', '1'),
		('creationdate', DATE_FORMAT(UTC_TIMESTAMP(), '%Y-%m-%dT%TZ')),
		('update_v1', DATE_FORMAT(UTC_TIMESTAMP(), '%Y-%m-%dT%TZ'));

END IF;

END$$ -- PROCUDURE up()

 delimiter ;

CALL up();

DROP PROCEDURE `up`;
