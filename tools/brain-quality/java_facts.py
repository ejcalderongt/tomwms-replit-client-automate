"""Export Java syntax and literal WS routes without executing Android code.

#EJC20261027_BRAIN_JAVA_AST
"""
import argparse
import hashlib
import json
import re
from pathlib import Path

WS_WRAPPERS = {'callMethod', 'callMethodJsonPost', 'callMethodJsonGet', 'callMethodPost'}


def descendants(node):
    stack = [node]
    while stack:
        current = stack.pop()
        yield current
        stack.extend(reversed(current.named_children))


def parse_file(path, root, parser):
    data = path.read_bytes()
    tree = parser.parse(data)
    relative = path.relative_to(root).as_posix()
    sha = hashlib.sha256(data).hexdigest()
    methods = []
    for method in descendants(tree.root_node):
        if method.type not in {'method_declaration', 'constructor_declaration'}:
            continue
        name_node = method.child_by_field_name('name')
        if not name_node:
            continue
        name = data[name_node.start_byte:name_node.end_byte].decode('utf-8')
        parent = method.parent
        owner = '<unknown>'
        while parent:
            if parent.type in {'class_declaration', 'interface_declaration', 'enum_declaration'}:
                type_name = parent.child_by_field_name('name')
                owner = data[type_name.start_byte:type_name.end_byte].decode('utf-8')
                break
            parent = parent.parent
        calls = []
        endpoints = []
        for call in descendants(method):
            if call.type != 'method_invocation':
                continue
            ancestor = call.parent
            while ancestor and ancestor.type not in {'method_declaration', 'constructor_declaration'}:
                ancestor = ancestor.parent
            if ancestor != method:
                continue
            target = call.child_by_field_name('name')
            arguments = call.child_by_field_name('arguments')
            qualifier = call.child_by_field_name('object')
            if not target:
                continue
            called = data[target.start_byte:target.end_byte].decode('utf-8')
            calls.append({'name': called, 'qualifier': data[qualifier.start_byte:qualifier.end_byte].decode('utf-8') if qualifier else None,
                          'argument_count': len(arguments.named_children) if arguments else 0,
                          'line': call.start_point.row + 1})
            if called in WS_WRAPPERS and arguments and arguments.named_children:
                first = arguments.named_children[0]
                if first.type == 'string_literal':
                    literal = data[first.start_byte:first.end_byte].decode('utf-8')[1:-1]
                    if re.fullmatch(r'[A-Za-z_]\w*', literal):
                        endpoints.append({'name': literal, 'wrapper': called, 'line': call.start_point.row + 1})
        parameters = method.child_by_field_name('parameters')
        methods.append({'id': f'{relative}:{method.start_point.row + 1}:{name}', 'path': relative,
                        'sha256': sha, 'type': owner, 'name': name,
                        'line_start': method.start_point.row + 1, 'line_end': method.end_point.row + 1,
                        'parameter_count': len(parameters.named_children) if parameters else 0,
                        'syntax_errors': int(tree.root_node.has_error), 'calls': calls, 'ws_endpoints': endpoints})
    return {'path': relative, 'sha256': sha, 'syntax_errors': int(tree.root_node.has_error)}, methods


def export(root, source_root, progress=False):
    from tree_sitter import Language, Parser
    import tree_sitter_java
    parser = Parser(Language(tree_sitter_java.language()))
    files, methods = [], []
    for path in sorted((root / source_root).rglob('*.java')):
        if progress:
            print(json.dumps({'parsing': path.relative_to(root).as_posix()}), flush=True)
        info, found = parse_file(path, root, parser)
        files.append(info)
        methods.extend(found)
    configuration = [{'path': name, 'sha256': hashlib.sha256((root / name).read_bytes()).hexdigest()}
                     for name in ['build.gradle', 'app/build.gradle', 'app/src/main/AndroidManifest.xml'] if (root / name).is_file()]
    return {'version': 1, 'repo_root': str(root), 'source_root': source_root, 'configuration_sources': configuration,
            'scope': 'Conventional Android Java source tree; Gradle variants not evaluated; literal WS routes only',
            'files': files, 'methods': methods}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', required=True, type=Path)
    parser.add_argument('--source-root', default='app/src/main/java')
    parser.add_argument('--output', required=True, type=Path)
    parser.add_argument('--progress', action='store_true')
    args = parser.parse_args()
    result = export(args.root.resolve(), args.source_root, args.progress)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'files': len(result['files']), 'methods': len(result['methods']),
                      'literal_ws_routes': sum(len(m['ws_endpoints']) for m in result['methods'])}))


if __name__ == '__main__':
    main()
