"""Create randomized paired review packets and keep method mapping separate.

#EJC20261027_BRAIN_BLIND_REVIEW
"""
import argparse
import copy
import json
import random
from pathlib import Path
from audit import digest


def pair_answers(baseline, coordinated, seed=20261027):
    first = {a['case_id']: a for a in baseline}
    second = {a['case_id']: a for a in coordinated}
    if set(first) != set(second) or len(first) != len(baseline) or len(second) != len(coordinated):
        raise ValueError('Answer case sets differ or contain duplicates')
    rng, packets, mapping = random.Random(seed), [], []
    for case_id in sorted(first):
        variants = [('baseline', first[case_id]), ('coordinated', second[case_id])]
        rng.shuffle(variants)
        for label, (method, answer) in zip(['A', 'B'], variants):
            blind = {key: copy.deepcopy(answer.get(key)) for key in ('case_id', 'scope', 'answer', 'claims', 'limitations')}
            claim_ids = {}
            for index, claim in enumerate(blind['claims'], 1):
                key = 'c' + str(index)
                claim_ids[key] = claim['id']
                claim['id'] = key
                claim.pop('review', None)
            packets.append({'case_id': case_id, 'variant': label, 'answer_record': blind})
            mapping.append({'case_id': case_id, 'variant': label, 'method': method, 'claim_ids': claim_ids})
    return packets, mapping


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('baseline', type=Path)
    parser.add_argument('coordinated', type=Path)
    parser.add_argument('--packets', required=True, type=Path)
    parser.add_argument('--mapping', required=True, type=Path)
    args = parser.parse_args()
    packets, mapping = pair_answers(json.loads(args.baseline.read_text(encoding='utf-8')),
                                    json.loads(args.coordinated.read_text(encoding='utf-8')))
    args.packets.parent.mkdir(parents=True, exist_ok=True)
    args.mapping.parent.mkdir(parents=True, exist_ok=True)
    args.packets.write_text(json.dumps({'label_policy': 'randomized within case; labels do not identify methods',
                                       'packets': packets}, ensure_ascii=False, indent=2), encoding='utf-8')
    args.mapping.write_text(json.dumps({'input_sha256': {'baseline': digest(args.baseline),
                                                        'coordinated': digest(args.coordinated)},
                                       'mapping': mapping}, indent=2), encoding='utf-8')
    print(json.dumps({'cases': len(mapping) // 2, 'variants': len(packets)}))


if __name__ == '__main__':
    main()
