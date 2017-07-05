SELECT
	`ruin_site`.`id` AS 'GS',
	`system`.`name` AS 'System',
	`ruin_type`.`name` AS 'Type',
	`body`.`name` AS 'Planet',
	`location`.`latitude` AS 'lat',
	`location`.`longitude` AS 'long'
	FROM `ruin_site`
	INNER JOIN `location` ON `ruin_site`.`location_id` = `location`.`id`
	INNER JOIN `body` ON `location`.`body_id` = `body`.`id`
	INNER JOIN `system` ON `location`.`system_id` = `system`.`id`
	INNER JOIN `ruin_type` on `ruin_site`.`ruintype_id` = `ruin_type`.`id`
	WHERE
		`ruin_site`.`id` NOT IN (SELECT DISTINCT `ruinsite_id` from `ruinsite_activeobelisks`);
