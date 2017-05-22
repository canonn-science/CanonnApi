SELECT
	`ruin_site`.`id` AS 'GS',
	`system`.`name` AS 'System',
	`ruin_type`.`name` AS 'Type',
	`body`.`name` AS 'Planet',
	`ruin_site`.`latitude` AS 'lat',
	`ruin_site`.`longitude` AS 'long'
	FROM `ruin_site`
	INNER JOIN `body` ON `ruin_site`.`body_id` = `body`.`id`
	INNER JOIN `system` ON `body`.`system_id` = `system`.`id`
	INNER JOIN `ruin_type` on `ruin_site`.`ruintype_id` = `ruin_type`.`id`
	WHERE
		`ruin_site`.`id` NOT IN (SELECT DISTINCT `ruinsite_id` from `ruinsite_activeobelisks`);
