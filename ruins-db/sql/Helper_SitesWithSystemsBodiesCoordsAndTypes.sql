SELECT
	CONCAT('GS', site.id) AS 'SiteID',
	system.name AS 'System',
	body.name AS 'Planet',
	site.latitude AS 'lat',
	site.longitude AS 'long',
	ruin_type.name AS 'Type',
	CONCAT('https://www.edsm.net/en/system/id/', system.edsm_ext_id, '/name/', system.name) AS 'edsm system link',
	CONCAT('https://www.edsm.net/en/system/bodies/id/', system.edsm_ext_id, '/name/', system.name, '/details/idB/', body.edsm_ext_id, '/nameB/', system.name, ' ' , body.name) AS 'edsm body link',
	CONCAT('https://eddb.io/system/', system.eddb_ext_id) AS 'eddb system link',
	CONCAT('https://eddb.io/body/', body.eddb_ext_id) AS 'eddb body link'
FROM ruin_site AS site
	INNER JOIN body ON site.body_id = body.id
	INNER JOIN system ON body.system_id = system.id
	INNER JOIN ruin_type on site.ruintype_id = ruin_type.id
ORDER BY
	site.id

