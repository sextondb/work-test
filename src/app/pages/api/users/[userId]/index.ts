import { NextApiRequest, NextApiResponse } from 'next'
import { sampleUserData } from '../../../../utils/sample-data'

const handler = (req: NextApiRequest, res: NextApiResponse) => {
    const {
        query: { userId },
    } = req

    const user = sampleUserData.find(x => x.id == Number(userId));
    if (user) {
        res.status(200).json(user)
    } else {
        res.status(404).end();
    }
}

export default handler