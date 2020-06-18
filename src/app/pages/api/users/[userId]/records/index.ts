import { NextApiRequest, NextApiResponse } from 'next'
import { sampleUserData, sampleBusinessContactRecordData } from '../../../../../utils/sample-data'

const handler = (req: NextApiRequest, res: NextApiResponse) => {
    const {
        query: { userId },
    } = req

    const user = sampleUserData.find(x => x.id == Number(userId));
    if (user) {
        const records = sampleBusinessContactRecordData.filter(x => x.userId === user.id);
        if (records) {
            res.status(200).json(records)
        } else {
            res.status(404).end();
        }
    } else {
        res.status(404).end();
    }
}

export default handler