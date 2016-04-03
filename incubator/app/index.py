import urllib as ur
from flask import Flask, render_template, jsonify
from sumy.parsers.html import HtmlParser
from sumy.parsers.plaintext import PlaintextParser
from sumy.nlp.tokenizers import Tokenizer
from sumy.summarizers.lsa import LsaSummarizer as Summarizer
from sumy.nlp.stemmers import Stemmer
from sumy.utils import get_stop_words

app = Flask(__name__)

@app.route('/')
def index():
    return render_template('index.html')

tasks = [
    {
        'id': 1,
        'title': u'Buy groceries',
        'description': u'Milk, Cheese, Pizza, Fruit, Tylenol', 
        'done': False
    },
    {
        'id': 2,
        'title': u'Learn Python',
        'description': u'Need to find a good Python tutorial on the web', 
        'done': False
    }
]

@app.route('/api/tasks', methods=['GET'])
def get_tasks():
    return jsonify({'tasks': tasks})

@app.route('/api/strings/<string:patent>', methods=['GET'])
def get_summary(patent):
    return jsonify({'summary': 'hello ' + patent})

@app.route('/api/patent/<string:id>', methods=['GET'])
def get_patent(id):
    url = 'https://s3.amazonaws.com/nasapatents/patents/patent_' + id
    data = ur.urlopen(url).read()
    summary = summarize(data)
    return summary


def summarize(str):
    parser = PlaintextParser.from_string(str, Tokenizer("english"))
    stemmer = Stemmer("english")
    summarizer = Summarizer(stemmer)
    summarizer.stop_words = get_stop_words("english")
    result = ''
    for sentence in summarizer(parser.document, 10):
       result += unicode(sentence)
    return result

if __name__ == '__main__':
    app.debug = True
    app.run()


