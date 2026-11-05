"""Extract declared metadata only; missing scope remains unknown.

#EJC20261027_BRAIN_METADATA
"""
import re
import yaml


class UniqueKeysLoader(yaml.SafeLoader):
    pass


def mapping(loader, node, deep=False):
    result = {}
    for key_node, value_node in node.value:
        key = loader.construct_object(key_node, deep=deep)
        if key in result:
            raise ValueError('duplicate-yaml-key')
        result[key] = loader.construct_object(value_node, deep=deep)
    return result


UniqueKeysLoader.add_constructor(yaml.resolver.BaseResolver.DEFAULT_MAPPING_TAG, mapping)


def declared_metadata(path):
    text = path.read_text(encoding='utf-8-sig')
    document = text
    if path.suffix.lower() in {'.md', '.markdown'}:
        match = re.match(r'\A---\s*\n(.*?)\n---(?:\s*\n|$)', text, flags=re.S)
        document = match.group(1) if match else ''
    issues = []
    try:
        data = yaml.load(document, Loader=UniqueKeysLoader) if document else {}
        if not isinstance(data, dict):
            data = {}
            issues.append('metadata-not-mapping')
    except (yaml.YAMLError, ValueError, TypeError):
        data = {}
        issues.append('invalid-or-duplicate-yaml')

    def scope(keys):
        values = []
        for key in keys:
            value = data.get(key)
            # Mapping schemas can contain prose, secrets, runtime aliases or notes.
            # Only explicit scalar/list scope is usable for filtering.
            items = value if isinstance(value, list) else [value] if isinstance(value, str) else []
            values.extend(v for v in items if isinstance(v, str) and v.strip())
        return sorted(set(values))

    clients = scope(['clients', 'clientes', 'cliente', 'clientes_destino'])
    branches = scope(['branches', 'ramas', 'ramas_afectadas', 'ramas_relevantes'])
    kind = data.get('kind') if data.get('kind') in {'fact', 'hypothesis', 'decision'} else 'unclassified'
    return {'clients': clients, 'branches': branches,
            'scope_state': 'declared' if clients and branches else 'incomplete',
            'kind': kind, 'validated_at': None,
            'declared_status': str(data.get('status', data.get('estado', 'unknown'))),
            'validation_state': 'not-independently-validated',
            'issues': issues}
