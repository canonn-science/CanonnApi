SELECT
	CONCAT('GS', `ruin_site`.`id`) AS `SiteID`,
	`system`.`name` AS `System`,
	`body`.`name` AS `Planet`,
	`location`.`latitude` AS `lat`,
	`location`.`longitude` AS `long`,
	`ruin_type`.`name` AS `Type`,
	CONCAT('https://www.edsm.net/en/system/id/', `system`.`edsm_ext_id`, '/name/', `system`.`name`) AS `edsm system link`,
	CONCAT('https://www.edsm.net/en/system/bodies/id/', `system`.`edsm_ext_id`, '/name/', `system`.`name`, '/details/idB/', `body`.`edsm_ext_id`, '/nameB/', `system`.`name`, ' ' , `body`.`name`) AS 'edsm body link',
	CONCAT('https://eddb.io/system/', `system`.`eddb_ext_id`) AS `eddb system link`,
	CONCAT('https://eddb.io/body/', `body`.`eddb_ext_id`) AS `eddb body link`
FROM `ruin_site`
	INNER JOIN `location` ON `location`.`id` = `ruin_site`.`id`
	INNER JOIN `body` ON `location`.`body_id` = `body`.`id`
	INNER JOIN `system` ON `location`.`system_id` = `system`.`id`
	INNER JOIN `ruin_type` on `ruin_site`.`ruintype_id` = `ruin_type`.`id`
ORDER BY
	`ruin_site`.`id`
